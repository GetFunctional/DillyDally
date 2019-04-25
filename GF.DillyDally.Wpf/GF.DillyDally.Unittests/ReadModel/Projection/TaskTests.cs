using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class TaskTests
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
        public async Task Creating_Task_ShouldCreateProjection()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<ICommandDispatcher>();
            var taskRepository = this._infrastructureSetup.DiContainer.GetInstance<ITaskRepository>();
            var categoryRepository = this._infrastructureSetup.DiContainer.GetInstance<ICategoryRepository>();
            var laneRepository = this._infrastructureSetup.DiContainer.GetInstance<ILaneRepository>();

            var exampleCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault();
            var exampleLane = (await laneRepository.GetAllAsync()).FirstOrDefault();

            var newTaskId = commandDispatcher.ExecuteCommand(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId, 5));
            var projection = await taskRepository.GetByIdAsync(newTaskId);

            // Assert
            Assert.That(newTaskId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(projection.TaskId, Is.EqualTo(newTaskId));
            Assert.That(projection, Is.Not.Null);
        }
    }
}
