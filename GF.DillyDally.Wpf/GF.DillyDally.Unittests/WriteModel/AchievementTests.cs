﻿using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class AchievementTests
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
        public async Task ChangeAchievementCounterValue_ChangesValue()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
            var createCommand = new CreateAchievementCommand("Test", 1, 3);
            var newAchievement = await commandDispatcher.Send(createCommand);
            var command = new ChangeAchievementCounterValueCommand(newAchievement.AchievementId, 6);

            // Act
            await commandDispatcher.Send(command);

            // Assert
            Assert.That(newAchievement != null, Is.True);
        }

        [Test]
        public async Task CompletingAchievement_CompletesAchievement()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
            var createCommand = new CreateAchievementCommand("Test", 1, 3);
            var newAchievement = await commandDispatcher.Send(createCommand);
            var command = new CompleteAchievementCommand(newAchievement.AchievementId);

            // Act
            await commandDispatcher.Send(command);

            // Assert
            Assert.That(newAchievement != null, Is.True);
        }

        [Test]
        public void Creating_Achievement_PersistsAcvm()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
            var command = new CreateAchievementCommand("Test", 1, 3);

            // Act
            var newAchievement = commandDispatcher.Send(command);

            // Assert
            Assert.That(newAchievement != null, Is.True);
        }
    }
}