using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
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
                var categoryRepository = this._infrastructureSetup.DiContainer.GetInstance<ICategoryRepository>();
                var laneRepository = this._infrastructureSetup.DiContainer.GetInstance<ILaneRepository>();

                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).FirstOrDefault();

                var task = await commandDispatcher.Send(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId));
                return task;
            }
        }

        [Test]
        public async Task Task_AttachImage()
        {
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var newTask = await this.CreateNewTask();
                var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResources", "TestImage.jpg");
                var fileInfo = new FileInfo(filePath);

                // Act
                var fileAttachResult = await commandDispatcher.Send(new AttachFileToTaskCommand(newTask.TaskId, filePath));

                Assert.That(fileInfo.Exists, Is.True);
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