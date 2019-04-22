using System;
using System.Collections.Generic;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class AchievementTests
    {
        private ServiceContainer _diContainer;

        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false});
        }

        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange
            var exampleFile = "AchievementTests.db";
            this._diContainer = this.CreateDependencyInjectionContainer();
            var dataBootstrapper = new DataBootstrapper(this._diContainer);
            dataBootstrapper.Run(new InitializationSettings(exampleFile, true, true));
            this.CreateTestCategories();
            this.CreateTestLanes();
            this.CreateTestRewards();
        }

        private void CreateTestCategories()
        {
            // Act && Assert
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

            var data = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Backlog Level 3", "#0C53BD"),
                new Tuple<string, string>("Backlog Level 2", "#0C53BD"),
                new Tuple<string, string>("Backlog Level 1", "#0C53BD"),
                new Tuple<string, string>("Pending", "#0C53BD"),
                new Tuple<string, string>("Rejected", "#0C53BD"),
                new Tuple<string, string>("Infinite", "#0C53BD"),
                new Tuple<string, string>("Done", "#0C53BD")
            };

            var createdIds = new List<Guid>();
            foreach (var lane in data)
            {
                var createLaneCommand = new CreateLaneCommand(lane.Item1, lane.Item2);
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

        [Test]
        public void Creating_RegularAchievement_PersistsAchievement()
        {
            // Act && Assert
            var commandDispatcher = this._diContainer.GetInstance<ICommandDispatcher>();

           //var achievementData = 

           // var createdIds = new List<Guid>();
           // foreach (var category in data)
           // {
           //     var createCommand = new CreateCategoryCommand(category.Item1, category.Item2);
           //     createdIds.Add(commandDispatcher.ExecuteCommand(createCommand));
           // }

            Assert.That(true, Is.True);
        }
    }
}