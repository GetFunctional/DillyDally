using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.Images.Repository;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.Shared.Images;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Domain.Tasks.Exceptions;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class TaskImageTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.Run(UnittestsSetup.ExampleDatabase);
        }

        #endregion

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();

        private async Task<CreateTaskResponse> CreateNewTask()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var categoryRepository = new CategoryRepository();
                var laneRepository = new LaneRepository();

                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).FirstOrDefault();

                var task = await commandDispatcher.Send(new CreateTaskCommand("Test", exampleCategory.CategoryId,
                    exampleLane.LaneId));
                return task;
            }
        }

        [Test]
        public async Task AttachingImage_ToTask_ShouldCreateProjection()
        {
            // Arrange
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var repository = new TaskImageRepository();
                var newTask = await this.CreateNewTask();
                var fileName = "TestImage.jpg";
                var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);

                var attachImageCommand = new AttachFileToTaskCommand(newTask.TaskId, filePath);
                await commandDispatcher.Send(attachImageCommand);

                // Act
                var projection = await repository.GetImagesForTaskAsync(connection, newTask.TaskId);

                // Assert
                Assert.That(newTask.TaskId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.Count, Is.EqualTo(3));
            }
        }

        [Test]
        public async Task Task_AssignPreviewImage_ReplacesImage()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var taskRepository = new TaskRepository();
                var imageRepository = new ImageRepository();

                var newTask = await this.CreateNewTask();
                var fileName = "TestImage.jpg";
                var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);

                var attachImageCommand = new AttachFileToTaskCommand(newTask.TaskId, filePath);
                var attachResult = await commandDispatcher.Send(attachImageCommand);

                // Act
                var replacePrimaryImageCommand = new AssignPreviewImageCommand(newTask.TaskId, attachResult.FileId);
                await commandDispatcher.Send(replacePrimaryImageCommand);

                // Assert
                var taskData = await taskRepository.GetByIdAsync(connection, newTask.TaskId);
                var imageData = await imageRepository.GetByOriginalFileIdAsync(connection, attachResult.FileId);
                Assert.That(taskData.PreviewImageFileId,
                    Is.EqualTo(imageData.Single(x => x.SizeType == ImageSizeType.PreviewSize).ImageId));
            }
        }

        [Test]
        public async Task UsingTwiceSameImage_ForDifferentTasks_ReusesImage()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var taskImagesRepository = new TaskImageRepository();
                var newTask = await this.CreateNewTask();
                var newTask2 = await this.CreateNewTask();
                var fileName = "TestImage.jpg";
                var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);

                var attachImageCommand = new AttachFileToTaskCommand(newTask.TaskId, filePath);
                var attachImageCommand2 = new AttachFileToTaskCommand(newTask2.TaskId, filePath);

                // Act
                var resultTask1 = await commandDispatcher.Send(attachImageCommand);
                var resultTask2 = await commandDispatcher.Send(attachImageCommand2);

                // Assert
                var task1Images = await taskImagesRepository.GetImagesForTaskAsync(connection, newTask.TaskId);
                var task2Images = await taskImagesRepository.GetImagesForTaskAsync(connection, newTask2.TaskId);

                Assert.That(resultTask1.FileId, Is.EqualTo(resultTask2.FileId));
                Assert.That(task1Images.Count, Is.EqualTo(task2Images.Count));
                Assert.That(task1Images.Select(x => x.ImageId).SequenceEqual(task2Images.Select(x => x.ImageId)),
                    Is.True);
            }
        }

        [Test]
        public async Task UsingTwiceSameImage_ThrowsException()
        {
            // Arrange
            var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
            var newTask = await this.CreateNewTask();
            var fileName = "TestImage.jpg";
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);

            var attachImageCommand = new AttachFileToTaskCommand(newTask.TaskId, filePath);
            await commandDispatcher.Send(attachImageCommand);
            Assert.ThrowsAsync<DuplicateAttachedFileException>(async () =>
                await commandDispatcher.Send(attachImageCommand));
        }
    }
}