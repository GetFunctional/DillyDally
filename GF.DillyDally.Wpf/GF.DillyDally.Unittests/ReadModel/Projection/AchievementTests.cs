using System;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Achievements.Repository;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class AchievementTests
    {
        #region SetupDatabase/Teardown

        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.SetupDatabase(UnittestsSetup.ExampleDatabase);
        }

        #endregion

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();

        [Test]
        public async Task Creating_Achievement_ShouldCreateProjection()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var repository = new AchievementRepository();
                var command = new CreateAchievementCommand("Test", 1, 5);

                // Act
                var newAchievement = await commandDispatcher.Send(command);
                var projection = await repository.GetByIdAsync(connection, newAchievement.AchievementId);

                // Assert
                Assert.That(newAchievement.AchievementId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.AchievementId, Is.EqualTo(newAchievement.AchievementId));
                Assert.That(projection.Name, Is.EqualTo(command.Name));
            }
        }

        [Test]
        public async Task CompletingAchievement_ShouldCompleteProjection()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var repository = new AchievementRepository();
                var command = new CreateAchievementCommand("Test", 1, 5);
                var newAchievement = await commandDispatcher.Send(command);
                var command2 = new CompleteAchievementCommand(newAchievement.AchievementId);

                // Act
                var completedResponse = await commandDispatcher.Send(command2);
                var projection = await repository.GetByIdAsync(connection, newAchievement.AchievementId);

                // Assert
                Assert.That(projection.CompletedOn, Is.Not.Null);
            }
        }
    }
}