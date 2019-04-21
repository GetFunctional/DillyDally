using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.WriteModel.Tests
{
    [TestFixture]
    public class InitialDataCreationTests
    {
        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false});
        }


        [Test]
        public void CreatingLanes_PersistsLanes()
        {
            // Arrange
            var exampleFile = "WriteModelTests.db";
            var diContainer = this.CreateDependencyInjectionContainer();
            var dataBootstrapper = new DataBootstrapper(diContainer);
            dataBootstrapper.Run(exampleFile);

            // Act && Assert
            var commandDispatcher = diContainer.GetInstance<ICommandDispatcher>();

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
    }
}