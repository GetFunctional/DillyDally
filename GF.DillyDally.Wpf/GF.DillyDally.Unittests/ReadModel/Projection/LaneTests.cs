using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using LightInject;
using MediatR;
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
            this._testInfrastructure.Run(Unittests.ExampleDatabase);
        }

        #endregion

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();

        [Test]
        public async Task Creating_RegularLane_ShouldCreateProjection()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var laneRepository = new LaneRepository();
                var name = "Test";
                var colorCode = "#123456";

                // Act
                var newLane = await commandDispatcher.Send(new CreateLaneCommand(name, colorCode, false, false));
                var laneFromProjection = await laneRepository.GetByIdAsync(connection, newLane.LaneId);

                // Assert
                Assert.That(newLane, Is.Not.EqualTo(Guid.Empty));
                Assert.That(laneFromProjection, Is.Not.Null);
                Assert.That(laneFromProjection.Name, Is.EqualTo(name));
                Assert.That(laneFromProjection.ColorCode, Is.EqualTo(colorCode));
            }
        }
    }
}