using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using LightInject;
using MediatR;

namespace GF.DillyDally.Unittests
{
    internal class DatabaseTestSetup
    {
        private readonly InfrastructureTestSetup _infrastructureSetup;
        private ServiceContainer _diContainer;

        public DatabaseTestSetup()
        {
            this._infrastructureSetup = new InfrastructureTestSetup();
        }

        public async Task SetupAsync(string exampleFile)
        {
            this._infrastructureSetup.Setup(exampleFile);
            this._diContainer = this._infrastructureSetup.DiContainer;
            await this.CreateNumberCounters();
            await this.CreateTestCategories();
            await this.CreateTestLanes();
            await this.CreateTestRewards();
        }

        private async Task CreateNumberCounters()
        {
            var commandDispatcher = this._diContainer.GetInstance<IMediator>();
            var createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Category, "CAT", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Task, "TSK", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Lane, "LN", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Achievement, "ACVM", 0);
            await commandDispatcher.Send(createCommand);
        }


        private async Task CreateTestCategories()
        {
            var commandDispatcher = this._diContainer.GetInstance<IMediator>();

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

        private async Task CreateTestLanes()
        {
            // Act && Assert
            var commandDispatcher = this._diContainer.GetInstance<IMediator>();

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

        private async Task CreateTestRewards()
        {
            // Act && Assert
            var commandDispatcher = this._diContainer.GetInstance<IMediator>();

            var data = new List<Tuple<string, string>>
                       {
                           new Tuple<string, string>("Gaming Time", "min."),
                           new Tuple<string, string>("Gaming Credits", "€"),
                           new Tuple<string, string>("Days off", "Tage"),
                           new Tuple<string, string>("Hearthstone Matches", "Matches")
                       };

            var createdIds = new List<Guid>();
            foreach (var lane in data)
            {
                var createCommand = new CreateRewardCommand(lane.Item1, lane.Item2);
                var createdReward = await commandDispatcher.Send(createCommand);
                createdIds.Add(createdReward.RewardId);
            }
        }


        public void DeleteDatabase()
        {
            this._diContainer.GetInstance<DatabaseFileHandler>().DeleteDatabaseIfExists();
        }
    }
}