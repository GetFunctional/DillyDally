using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class TaskTests
    {
        private RealTestSetup _realTestSetup;

        

        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange
            var exampleFile = "AchievementTests.db";
            this._realTestSetup = new RealTestSetup();
            this._realTestSetup.Setup(exampleFile);
        }

        

        [Test]
        public async Task Creating_Task_PersistsTask()
        {
            // Arrange
            var commandDispatcher = this._realTestSetup.GetInstance<ICommandDispatcher>();
            var categoryRepository = this._realTestSetup.GetInstance<ICategoryRepository>();
            var laneRepository = this._realTestSetup.GetInstance<ILaneRepository>();

            var exampleCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault();
            var exampleLane = (await laneRepository.GetAllAsync()).FirstOrDefault();

            var newTaskId = commandDispatcher.ExecuteCommand(new CreateTaskCommand("Test", exampleCategory.CategoryId, exampleLane.LaneId, 5));

            Assert.That(newTaskId != null, Is.True);
        }
    }
}