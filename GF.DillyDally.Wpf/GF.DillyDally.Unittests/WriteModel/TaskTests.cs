using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class TaskTests
    {
        private readonly InfrastructureTestSetup _infrastructureSetup = new InfrastructureTestSetup();

        [SetUp]
        public void Setup()
        {
            this._infrastructureSetup.Setup(UnittestsSetup.ExampleDatabase);
        }

        [Test]
        public async Task Task_Create()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<ICommandDispatcher>();
            var categoryRepository = this._infrastructureSetup.DiContainer.GetInstance<ICategoryRepository>();
            var laneRepository = this._infrastructureSetup.DiContainer.GetInstance<ILaneRepository>();

            var exampleCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault();
            var exampleLane = (await laneRepository.GetAllAsync()).FirstOrDefault();

            var newTaskId = commandDispatcher.ExecuteCommand(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId));

            Assert.That(newTaskId != null, Is.True);
        }

        [Test]
        public async Task Task_AttachImage()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<ICommandDispatcher>();
            var categoryRepository = this._infrastructureSetup.DiContainer.GetInstance<ICategoryRepository>();
            var laneRepository = this._infrastructureSetup.DiContainer.GetInstance<ILaneRepository>();
            var exampleCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault();
            var exampleLane = (await laneRepository.GetAllAsync()).FirstOrDefault();
            var newTaskId = commandDispatcher.ExecuteCommand(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId));

            // Act
            var fileInfo = new FileInfo("asdf");
            //fileInfo.has
            //var imageData
            //var attachImageCommand = new AttachImageToTaskCommand(newTaskId, )
            Assert.Fail();
        }
    }
}