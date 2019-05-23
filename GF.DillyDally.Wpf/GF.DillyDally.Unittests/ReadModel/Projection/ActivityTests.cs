﻿using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.WriteModel.Domain.Activities.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class ActivityTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._infrastructureSetup.Setup(UnittestsSetup.ExampleDatabase);
        }

        #endregion

        private readonly InfrastructureTestSetup _infrastructureSetup = new InfrastructureTestSetup();

        [Test]
        public async Task Creating_Activity_ShouldCreateProjection()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var repository = new ActivityRepository();
                var command = new CreatePercentageActivityCommand("Test");

                // Act
                var newActivity = await commandDispatcher.Send(command);
                var projection = await repository.GetByIdAsync(connection, newActivity.ActivityId);

                // Assert
                Assert.That(newActivity.ActivityId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.ActivityId, Is.EqualTo(newActivity.ActivityId));
                Assert.That(projection.Name, Is.EqualTo(command.Name));
            }
        }
    }
}