using System;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Achievements.Repository;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
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
        public async Task Changing_AchievementCounterValue_ShouldChangeProjection()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var repository = new AchievementRepository();
                var command = new CreateAchievementCommand("Test", 1, 5);
                var newAchievement = await commandDispatcher.Send(command);

                // Act
                var changeCommand = new ChangeAchievementCounterValueCommand(newAchievement.AchievementId, 5);
                await commandDispatcher.Send(changeCommand);
                var projection = await repository.GetByIdAsync(connection, newAchievement.AchievementId);

                // Assert
                Assert.That(newAchievement.AchievementId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.AchievementId, Is.EqualTo(newAchievement.AchievementId));
                Assert.That(projection.Name, Is.EqualTo(command.Name));
                Assert.That(projection.StoryPoints, Is.EqualTo(command.Storypoints));
                Assert.That(projection.CounterIncrease, Is.EqualTo(changeCommand.NewCounterValue));
            }
        }

        [Test]
        public async Task Changing_AchievementCounterValue_ShouldNotChangeOtherProjections()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var repository = new AchievementRepository();
                var command = new CreateAchievementCommand("Test", 1, 5);
                var newAchievement = await commandDispatcher.Send(command);
                var newAchievement2 = await commandDispatcher.Send(command);

                // Act
                var changeCommand = new ChangeAchievementCounterValueCommand(newAchievement.AchievementId, 5);
                await commandDispatcher.Send(changeCommand);
                var projection = await repository.GetByIdAsync(connection, newAchievement.AchievementId);
                var projection2 = await repository.GetByIdAsync(connection, newAchievement2.AchievementId);

                // Assert
                Assert.That(projection2.CounterIncrease, Is.EqualTo(command.CounterIncrease));
                Assert.That(projection.CounterIncrease, Is.EqualTo(5));
            }
        }


        [Test]
        public async Task Completing_Achievement_ShouldCreateProjection()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var createCommand = new CreateAchievementCommand("Test", 1, 3);
                var newAchievement = await commandDispatcher.Send(createCommand);
                var command = new CompleteAchievementCommand(newAchievement.AchievementId);
                var repository = new AchievementCompletionRepository();
                var timeStampBeforeCompletion = DateTime.Now;

                // Act
                await commandDispatcher.Send(command);
                var projection = await repository.GetAchievementCompletionsAsync(connection, newAchievement.AchievementId);
                var singleEntry = projection.FirstOrDefault();

                // Assert
                Assert.That(newAchievement.AchievementId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.Count, Is.EqualTo(1));
                Assert.That(singleEntry.Storypoints, Is.EqualTo(createCommand.Storypoints));
                Assert.That(singleEntry.CounterIncreaseValue, Is.EqualTo(createCommand.CounterIncrease));
                Assert.That(singleEntry.CompletedOn, Is.GreaterThan(timeStampBeforeCompletion));
            }
        }

        [Test]
        public async Task Creating_Achievement_ShouldCreateProjection()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
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
                Assert.That(projection.StoryPoints, Is.EqualTo(command.Storypoints));
                Assert.That(projection.CounterIncrease, Is.EqualTo(command.CounterIncrease));
            }
        }
    }
}