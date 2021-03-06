﻿using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class RunningNumberTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.Run(Unittests.ExampleDatabase);
        }

        #endregion

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();


        [Test]
        public async Task RunningNumber_GetNext_ShouldIncreaseProjection()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var runningNumberRepository = new RunningNumberRepository();
                var runningNumberCounterRepository = new RunningNumberCounterRepository();

                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var nextNumberCommand = new CreateRunningNumberCommand(RunningNumberCounterArea.Achievement);
                var achievementCounterId =
                    RunningNumberCounterCommandHandler.AreaToIdentityMapping[RunningNumberCounterArea.Achievement];
                var runningNumberBefore =
                    await runningNumberCounterRepository.GetByIdAsync(connection, achievementCounterId);

                // Act
                var attachResult = await commandDispatcher.Send(nextNumberCommand);
                var newNumber = await runningNumberRepository.GetByIdAsync(connection, attachResult.RunningNumberId);
                var runningNumberAfter =
                    await runningNumberCounterRepository.GetByIdAsync(connection, achievementCounterId);

                // Assert
                Assert.That(newNumber, Is.Not.Null);
                Assert.That(newNumber.RunningNumberCounterId, Is.EqualTo(achievementCounterId));
                Assert.That(runningNumberAfter.CurrentNumber, Is.GreaterThan(runningNumberBefore.CurrentNumber));
            }
        }
    }
}