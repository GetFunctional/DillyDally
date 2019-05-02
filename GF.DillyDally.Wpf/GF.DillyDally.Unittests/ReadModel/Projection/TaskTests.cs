using System;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class TaskTests
    {
        #region SetupAsync/Teardown

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
            using (var connection = this._infrastructureSetup.OpenDatabaseConnection())
            {
                var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
                var repository = this._infrastructureSetup.DiContainer.GetInstance<ITaskRepository>();
                var categoryRepository = this._infrastructureSetup.DiContainer.GetInstance<ICategoryRepository>();
                var laneRepository = this._infrastructureSetup.DiContainer.GetInstance<ILaneRepository>();
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