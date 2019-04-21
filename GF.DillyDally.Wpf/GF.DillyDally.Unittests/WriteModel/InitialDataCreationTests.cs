using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Domain.Rewards;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class InitialDataCreationTests
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
            var exampleFile = "InitialDataTests.db";
            this._diContainer = this.CreateDependencyInjectionContainer();
            var dataBootstrapper = new DataBootstrapper(this._diContainer);
            dataBootstrapper.Run(new InitializationSettings(exampleFile, true, true));
        }

        [Test]
        public void Creating_Lanes_PersistsLanes()
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

            Assert.That(createdIds.Count, Is.EqualTo(7));
            Assert.That(createdIds.All(x => Guid.Empty != x), Is.True);
        }

        [Test]
        public void Creating_Rewards_PersistsRewards()
        {
            // Act && Assert
            var commandDispatcher = this._diContainer.GetInstance<ICommandDispatcher>();

            var data = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Gaming Time", "min."),
                new Tuple<string, string>("Gaming Credits", "€"),
                new Tuple<string, string>("Days off", "Tage"),
                new Tuple<string, string>("Hearthstone Matches", "Matches"),
            };

            var createdIds = new List<Guid>();
            foreach (var lane in data)
            {
                var createCommand = new CreateRewardCommand(lane.Item1, lane.Item2);
                createdIds.Add(commandDispatcher.ExecuteCommand(createCommand));
            }

            Assert.That(createdIds.Count, Is.EqualTo(4));
            Assert.That(createdIds.All(x => Guid.Empty != x), Is.True);
        }
    }
}