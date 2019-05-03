using System;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using LightInject;
using MediatR;
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

        private async Task<CreateTaskResponse> CreateNewTask()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var categoryRepository = new CategoryRepository();
                var laneRepository = new LaneRepository();

                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).FirstOrDefault();

                var task = await commandDispatcher.Send(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId));
                return task;
            }
        }

        [Test]
        public async Task Creating_Task_ShouldCreateProjection()
        {
            // Arrange
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var repository = new TaskRepository();
                var categoryRepository = new CategoryRepository();
                var laneRepository = new LaneRepository();
                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).FirstOrDefault();
                var timeStampBeforeCreation = DateTime.Now;
                var command = new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId);

                // Act
                var newTask = await commandDispatcher.Send(command);
                var projection = await repository.GetByIdAsync(connection, newTask.TaskId);

                // Assert
                Assert.That(newTask.TaskId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.TaskId, Is.EqualTo(newTask.TaskId));
                Assert.That(projection.Name, Is.EqualTo(command.Name));
                Assert.That(projection.CategoryId, Is.EqualTo(exampleCategory.CategoryId));
                Assert.That(projection.LaneId, Is.EqualTo(exampleLane.LaneId));
                Assert.That(projection.CreatedOn, Is.GreaterThan(timeStampBeforeCreation));
                Assert.That(projection.DueDate, Is.Null);
            }
        }
    }
}