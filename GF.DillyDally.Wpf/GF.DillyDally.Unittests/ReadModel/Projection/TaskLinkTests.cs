using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class TaskLinkTests
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
        public async Task LinkingTask_ShouldCreateProjection()
        {
            // Arrange
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var taskLinksRepository = this._infrastructureSetup.DiContainer.GetInstance<ITaskLinksRepository>();
                var newTask1 = await this.CreateNewTask();
                var newTask2 = await this.CreateNewTask();

                // Act
                var result = await commandDispatcher.Send(new LinkTaskCommand(newTask1.TaskId, newTask2.TaskId));

                // Assert
                var links = await taskLinksRepository.GetLinksForTaskIdAsync(connection ,newTask1.TaskId);
                var links2 = await taskLinksRepository.GetLinksForTaskIdAsync(connection ,newTask2.TaskId);

                Assert.That(links.Count, Is.EqualTo(1));
                Assert.That(links2.Count, Is.EqualTo(1));
                Assert.That(links.Single().LinkedTaskId, Is.EqualTo(newTask2.TaskId));
                Assert.That(links2.Single().LinkedTaskId, Is.EqualTo(newTask1.TaskId));
            }
        }
    }
}