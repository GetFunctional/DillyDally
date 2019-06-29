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

namespace GF.DillyDally.Unittests.Core
{
    internal class DatabaseTestSetup
    {
        private readonly TestInfrastructure _testInfrastructure;
        private ServiceContainer _diContainer;

        public DatabaseTestSetup()
        {
            this._testInfrastructure = new TestInfrastructure();
        }

        public async Task SetupAsync(string exampleFile)
        {
            await this._testInfrastructure.SetupDatabaseAsync(exampleFile);

            this._diContainer = this._testInfrastructure.DiContainer;
            await this.CreateTestCategories();
            await this.CreateTestRewards();
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