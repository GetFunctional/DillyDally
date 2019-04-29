using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
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
        public void Creating_Achievement_PersistsAcvm()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<ICommandDispatcher>();
            var command = new CreateAchievementCommand("Test", 1, 3);

            // Act
            var newAchievement = commandDispatcher.ExecuteCommand(command);

            // Assert
            Assert.That(newAchievement != null, Is.True);
        }
    }
}