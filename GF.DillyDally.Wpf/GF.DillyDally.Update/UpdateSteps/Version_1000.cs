using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Activities;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using MediatR;

namespace GF.DillyDally.Update.UpdateSteps
{
    internal class Version_1000 : IUpdateStep
    {
        #region IUpdateStep Members

        public Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public IList<string> GetSqlUpdateScripts()
        {
            var sqlScriptSelector = new SqlScriptSelector();
            return sqlScriptSelector.GetScriptsForVersion(this.Version);
        }

        public async Task PerformMigrationsAsync(IMediator mediator)
        {
            await this.CreateMandatoryData(mediator);
        }

        #endregion

        public async Task CreateMandatoryData(IMediator mediator)
        {
            await this.InitializeNumberCounters(mediator);
            await this.InitializeLanes(mediator);
            await this.InitializeActivities(mediator);
            await this.CreateTestCategories(mediator);
        }

        private async Task InitializeActivities(IMediator commandDispatcher)
        {
            var activityService = new ActivityService(commandDispatcher);
            await activityService.CreateActivityList();
        }

        private async Task InitializeLanes(IMediator commandDispatcher)
        {
            var data = new List<Tuple<string, string, bool, bool>>
            {
                new Tuple<string, string, bool, bool>("Undefined", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Defined", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Stories", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Ready", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Sprint", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Rejected", "#0C53BD", false, true),
                new Tuple<string, string, bool, bool>("Done", "#0C53BD", true, false)
            };

            var createdIds = new List<Guid>();
            foreach (var lane in data)
            {
                var createLaneCommand = new CreateLaneCommand(lane.Item1, lane.Item2, lane.Item3, lane.Item4);
                var createdLane = await commandDispatcher.Send(createLaneCommand);
                createdIds.Add(createdLane.LaneId);
            }
        }

        private async Task InitializeNumberCounters(IMediator commandDispatcher)
        {
            var createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Category, "CAT", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Task, "TSK", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Lane, "LN", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Achievement, "ACVM", 0);
            await commandDispatcher.Send(createCommand);
        }

        private async Task CreateTestCategories(IMediator commandDispatcher)
        {
            var data = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Gaming", "#00AEEF"),
                new Tuple<string, string>("Fitness", "#FECF39"),
                new Tuple<string, string>("Haushalt", "#FFDF6D"),
                new Tuple<string, string>("Essen", "#74C7A8"),
                new Tuple<string, string>("Do it yourself", "#8781BD"),
                new Tuple<string, string>("Beziehung", "#81736A"),
                new Tuple<string, string>("Neuer Horizont", "#D65B4A"),
                new Tuple<string, string>("Notwendiges", "#66B92E"),
                new Tuple<string, string>("Programming", "#DA932C"),
                new Tuple<string, string>("Lifestyle", "#0C53BD")
            };

            var createdIds = new List<Guid>();
            foreach (var category in data)
            {
                var createCommand = new CreateCategoryCommand(category.Item1, category.Item2);
                var createdCat = await commandDispatcher.Send(createCommand);
                createdIds.Add(createdCat.CategoryId);
            }
        }
    }
}