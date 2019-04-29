using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
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
        public async Task Creating_Achievement_ShouldCreateProjection()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<ICommandDispatcher>();
            var repository = this._infrastructureSetup.DiContainer.GetInstance<IAchievementRepository>();
            var command = new CreateAchievementCommand("Test", 1, 5);

            var newId = commandDispatcher.ExecuteCommand(command);
            var projection = await repository.GetByIdAsync(newId);

            // Assert
            Assert.That(newId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(projection, Is.Not.Null);
            Assert.That(projection.AchievementId, Is.EqualTo(newId));
            Assert.That(projection.Name, Is.EqualTo(command.Name));
            Assert.That(projection.StoryPoints, Is.EqualTo(command.Storypoints));
            Assert.That(projection.CounterIncrease, Is.EqualTo(command.CounterIncrease));
        }
    }
}