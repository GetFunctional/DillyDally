using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.Files.Repository;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.Shared.Extensions;
using GF.DillyDally.WriteModel.Domain.Tasks;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Domain.Tasks.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
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

                var task = await commandDispatcher.Send(new CreateTaskCommand("Prepare Q2 Overview of Mixpanel Statistics (Keynote required)", exampleCategory.CategoryId, exampleLane.LaneId));
                return task;
            }
        }

        [Test]
        public async Task Task_AssignPreviewImage_ReplacesImage()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
            var newTask = await this.CreateNewTask();
            var fileName = "TestImage.jpg";
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);

            var attachImageCommand = new AttachFileToTaskCommand(newTask.TaskId, filePath);
            var attachResult = await commandDispatcher.Send(attachImageCommand);

            var replacePrimaryImageCommand = new AssignPreviewImageCommand(newTask.TaskId, attachResult.FileId);
            await commandDispatcher.Send(replacePrimaryImageCommand);
        }

        [TestCase("Do your Job")]
        [TestCase(null)]
        public async Task Task_AssignDefinitionOfDone_ShouldSuccess(string definitionOfDone)
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
            var newTask = await this.CreateNewTask();

            var definitionOfDoneCommand = new AssignDefinitionOfDoneCommand(newTask.TaskId, definitionOfDone);

            // Act
            var result = await commandDispatcher.Send(definitionOfDoneCommand);
            var aggregateRepository = this._infrastructureSetup.DiContainer.GetInstance<IAggregateRepository>();
            var afterChanges = aggregateRepository.GetById<TaskAggregateRoot>(newTask.TaskId);

            // Assert
            Assert.That(afterChanges.DefinitionOfDone, Is.EqualTo(definitionOfDone));
        }

        [TestCase(" ")]
        [TestCase("")]
        public async Task Task_AssignDefinitionOfDone_ShouldFail(string definitionOfDone)
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
            var newTask = await this.CreateNewTask();

            // Act
            var definitionOfDoneCommand = new AssignDefinitionOfDoneCommand(newTask.TaskId, definitionOfDone);

            // Assert & Act
            Assert.ThrowsAsync<InvalidDefinitionOfDoneException>(async () => await commandDispatcher.Send(definitionOfDoneCommand));
        }

        [Test]
        public async Task UsingTwiceSameImage_ForDifferentTasks_ReusesImage()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
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
            Assert.That(resultTask1.FileId, Is.EqualTo(resultTask2.FileId));
        }

        [Test]
        public async Task Task_AttachImage()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var fileRepository = new FileRepository();

                var newTask = await this.CreateNewTask();
                var fileName = "TestImage.jpg";
                var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", fileName);
                var fileInfo = new FileInfo(filePath);

                // Act
                var fileAttachResult = await commandDispatcher.Send(new AttachFileToTaskCommand(newTask.TaskId, filePath));

                // Assert
                var fileInStore = await fileRepository.GetByIdAsync(connection, fileAttachResult.FileId);

                Assert.That(fileInfo.Exists, Is.True);
                Assert.That(fileInStore.Name, Is.EqualTo(fileName));
                Assert.That(fileInStore.IsImage, Is.EqualTo(true));
                Assert.That(fileAttachResult.FileId, Is.Not.EqualTo(Guid.Empty));
            }
        }

        [Test]
        public async Task Task_Create()
        {
            // Act
            var newTask = await this.CreateNewTask();

            // Assert
            Assert.That(newTask != null, Is.True);
            Assert.That(newTask.TaskId, Is.Not.EqualTo(Guid.Empty));
        }
    }
}