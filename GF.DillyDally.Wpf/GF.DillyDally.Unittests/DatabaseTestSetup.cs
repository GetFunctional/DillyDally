using System;
using System.Collections.Generic;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;

namespace GF.DillyDally.Unittests
{
    internal class DatabaseTestSetup
    {
        private readonly InfrastructureTestSetup _infrastructureSetup;
        private ServiceContainer _diContainer;

        public DatabaseTestSetup() => this._infrastructureSetup = new InfrastructureTestSetup();

        public void Setup(string exampleFile)
        {
            this._infrastructureSetup.Setup(exampleFile);
            this._diContainer = this._infrastructureSetup.DiContainer;
            this.CreateNumberCounters();
            this.CreateTestCategories();
            this.CreateTestLanes();
            this.CreateTestRewards();
        }

        private void CreateNumberCounters()
        {
            var commandDispatcher = this._diContainer.GetInstance<ICommandDispatcher>();
            var createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Category, "CAT", 0);
            commandDispatcher.ExecuteCommand(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Task, "TSK", 0);
            commandDispatcher.ExecuteCommand(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Lane, "LN", 0);
            commandDispatcher.ExecuteCommand(createCommand);
        }


        private void CreateTestCategories()
        {
            var commandDispatcher = this._diContainer.GetInstance<ICommandDispatcher>();

            var data = new List<Tuple<string, string>>
                       {
                           new Tuple<string, string>("Gaming", "#0C53BD"),
                           new Tuple<string, string>("Fitness", "#0C53BD"),
                           new Tuple<string, string>("Haushalt", "#0C53BD"),
                           new Tuple<string, string>("Essen", "#0C53BD"),
                           new Tuple<string, string>("Do it yourself", "#0C53BD"),
                           new Tuple<string, string>("Beziehung", "#0C53BD"),
                           new Tuple<string, string>("Neuer Horizont", "#0C53BD"),
                           new Tuple<string, string>("Notwendiges", "#0C53BD"),
                           new Tuple<string, string>("Programming", "#0C53BD"),
                           new Tuple<string, string>("Lifestyle", "#0C53BD")
                       };

            var createdIds = new List<Guid>();
            foreach (var category in data)
            {
                var createCommand = new CreateCategoryCommand(category.Item1, category.Item2);
                createdIds.Add(commandDispatcher.ExecuteCommand(createCommand));
            }
        }

        private void CreateTestLanes()
        {
            // Act && Assert
            var commandDispatcher = this._diContainer.GetInstance<ICommandDispatcher>();

            var data = new List<Tuple<string, string, bool,bool>>
                       {
                           new Tuple<string, string, bool,bool>("Backlog Level 3", "#0C53BD",false,false),
                           new Tuple<string, string, bool,bool>("Backlog Level 2", "#0C53BD",false,false),
                           new Tuple<string, string, bool,bool>("Backlog Level 1", "#0C53BD",false,false),
                           new Tuple<string, string, bool,bool>("Pending", "#0C53BD",false,false),
                           new Tuple<string, string, bool,bool>("Rejected", "#0C53BD",false, true),
                           new Tuple<string, string, bool,bool>("Infinite", "#0C53BD", false,false),
                           new Tuple<string, string, bool,bool>("Done", "#0C53BD", true, false)
                       };

            var createdIds = new List<Guid>();
            foreach (var lane in data)
            {
                var createLaneCommand = new CreateLaneCommand(lane.Item1, lane.Item2, lane.Item3, lane.Item4);
                createdIds.Add(commandDispatcher.ExecuteCommand(createLaneCommand));
            }
        }

        private void CreateTestRewards()
        {
            // Act && Assert
            var commandDispatcher = this._diContainer.GetInstance<ICommandDispatcher>();

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
                createdIds.Add(commandDispatcher.ExecuteCommand(createCommand));
            }
        }


        public void DeleteDatabase()
        {
            this._diContainer.GetInstance<DatabaseFileHandler>().DeleteDatabaseIfExists();
        }
    }
}