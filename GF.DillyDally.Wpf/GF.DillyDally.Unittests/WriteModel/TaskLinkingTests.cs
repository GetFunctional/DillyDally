using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Tasks;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class TaskLinkingTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.Setup(UnittestsSetup.ExampleDatabase);
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

                var task = await commandDispatcher.Send(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId));
                return task;
            }
        }

        [Test]
        public async Task Task_LinksTask_BothAreLinked()
        {
            // Arrange
            var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
            var aggregateRepository = this._testInfrastructure.DiContainer.GetInstance<IAggregateRepository>();
            var newTask1 = await this.CreateNewTask();
            var newTask2 = await this.CreateNewTask();

            // Act
            var result = await commandDispatcher.Send(new LinkTaskCommand(newTask1.TaskId, newTask2.TaskId));

            // Assert
            var task1 = aggregateRepository.GetById<TaskAggregateRoot>(newTask1.TaskId);
            var task2 = aggregateRepository.GetById<TaskAggregateRoot>(newTask2.TaskId);

            Assert.That(task1.LinkedTasks.Contains(task2.AggregateId), Is.True);
            Assert.That(task2.LinkedTasks.Contains(task1.AggregateId), Is.True);
        }
    }
}