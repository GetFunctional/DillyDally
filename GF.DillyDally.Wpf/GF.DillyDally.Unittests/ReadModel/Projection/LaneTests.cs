using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class LaneTests
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
        public async Task Creating_RegularLane_ShouldCreateProjection()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<ICommandDispatcher>();
            var laneRepository = this._infrastructureSetup.DiContainer.GetInstance<ILaneRepository>();
            var name = "Test";
            var colorCode = "#123456";

            // Act
            var newLaneId = commandDispatcher.ExecuteCommand(new CreateLaneCommand(name, colorCode, false, false));
            var laneFromProjection = await laneRepository.GetByIdAsync(newLaneId);

            // Assert
            Assert.That(newLaneId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(laneFromProjection, Is.Not.Null);
            Assert.That(laneFromProjection.Name, Is.EqualTo(name));
            Assert.That(laneFromProjection.ColorCode, Is.EqualTo(colorCode));
        }
    }
}