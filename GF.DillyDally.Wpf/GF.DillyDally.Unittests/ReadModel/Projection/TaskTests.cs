using System;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.Shared.Extensions;
using GF.DillyDally.WriteModel.Domain.Tasks;
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

                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();

                var task = await commandDispatcher.Send(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId));
                return task;
            }
        }

        [TestCase("Do the fuckin job")]
        public async Task Setting_DefinitionOfDone_ShouldCreateProjection(string definitionOfDone)
        {
            // Arrange
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var repository = new TaskRepository();
                var newTask = await this.CreateNewTask();

                // Act
                var definitionOfDoneCommand = new AssignDefinitionOfDoneCommand(newTask.TaskId, definitionOfDone);
                await commandDispatcher.Send(definitionOfDoneCommand);

                // Assert
                var projection = await repository.GetByIdAsync(connection, newTask.TaskId);

                // Assert
                Assert.That(projection.DefinitionOfDone, Is.Not.Null);
                Assert.That(projection.DefinitionOfDone, Is.EqualTo(definitionOfDone));
            }
        }

        [Test]
        public async Task Creating_Task_ShouldCreate_TaskLaneProjection()
        {
            // Arrange
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var repository = new TaskRepository();
                var categoryRepository = new CategoryRepository();
                var laneRepository = new LaneRepository();
                var laneTaskRepository = new LaneTaskRepository();
                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var timeStampBeforeCreation = DateTime.Now;
                var taskName = "Test";
                var taskService = new TaskService(commandDispatcher);

                // Act
                var newTask = await taskService.CreateNewTaskAsync(taskName,exampleCategory.CategoryId, exampleLane.LaneId);
                var projection = await repository.GetByIdAsync(connection, newTask.TaskId);
                var createdLink = await laneTaskRepository.GetLaneTaskByTaskId(connection, newTask.TaskId);

                // Assert
                Assert.That(newTask.TaskId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(createdLink, Is.Not.Null);
                Assert.That(createdLink.TaskId, Is.EqualTo(newTask.TaskId));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.TaskId, Is.EqualTo(newTask.TaskId));
                Assert.That(projection.Name, Is.EqualTo(taskName));
                Assert.That(projection.CategoryId, Is.EqualTo(exampleCategory.CategoryId));
                Assert.That(projection.CreatedOn, Is.GreaterThan(timeStampBeforeCreation));
                Assert.That(projection.DueDate, Is.Null);
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
                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var timeStampBeforeCreation = DateTime.Now;
                var taskService = new TaskService(commandDispatcher);
                var taskName = "Test";

                // Act
                var newTask = await taskService.CreateNewTaskAsync(taskName,exampleCategory.CategoryId, exampleLane.LaneId);
                var projection = await repository.GetByIdAsync(connection, newTask.TaskId);

                // Assert
                Assert.That(newTask.TaskId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.TaskId, Is.EqualTo(newTask.TaskId));
                Assert.That(projection.Name, Is.EqualTo(taskName));
                Assert.That(projection.CategoryId, Is.EqualTo(exampleCategory.CategoryId));
                Assert.That(projection.CreatedOn, Is.GreaterThan(timeStampBeforeCreation));
                Assert.That(projection.DueDate, Is.Null);
            }
        }

        [Test]
        public async Task Creating_Task_ShouldNotThrowException()
        {
            // Arrange
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var categoryRepository = new CategoryRepository();
                var laneRepository = new LaneRepository();
                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var taskService = new TaskService(commandDispatcher);

                // Act
                Assert.DoesNotThrowAsync(async () => await taskService.CreateNewTaskAsync("Test",exampleCategory.CategoryId, exampleLane.LaneId));
            }
        }
    }
}