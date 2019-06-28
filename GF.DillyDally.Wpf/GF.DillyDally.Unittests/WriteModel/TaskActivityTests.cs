﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.Shared.Extensions;
using GF.DillyDally.WriteModel.Domain.Activities;
using GF.DillyDally.WriteModel.Domain.Tasks;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class TaskActivityTests
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
                var taskService = this._infrastructureSetup.DiContainer.GetInstance<TaskService>();
                var categoryRepository = new CategoryRepository();
                var laneRepository = new LaneRepository();

                var exampleCategory = (await categoryRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();
                var exampleLane = (await laneRepository.GetAllAsync(connection)).Shuffle().FirstOrDefault();

                var task = await taskService.CreateNewTaskAsync("Prepare Q2 Overview of Mixpanel Statistics (Keynote required)",
                    exampleCategory.CategoryId, exampleLane.LaneId);
                return task;
            }
        }

        [Test]
        public async Task Task_CreateAndAddActivities()
        {
            // Arrange
            var activityService = this._infrastructureSetup.DiContainer.GetInstance<ActivityService>();
            var taskService = this._infrastructureSetup.DiContainer.GetInstance<TaskService>();
            var aggregateRepository = this._infrastructureSetup.DiContainer.GetInstance<IAggregateRepository>();

            var task = await this.CreateNewTask();
            var activity = await activityService.CreatePercentageActivityAsync("Test1234");
            var activity2 = await activityService.CreatePercentageActivityAsync("862637328");
            var activitySet = new HashSet<Guid>(new[] {activity.ActivityId, activity2.ActivityId});

            // Act
            var linkResult = taskService.LinkTaskToActivitiesAsync(task.TaskId, activitySet);

            // Assert
            var taskAggregate = aggregateRepository.GetById<TaskAggregateRoot>(task.TaskId);

            Assert.That(taskAggregate.LinkedActivities.Count(), Is.EqualTo(2));
        }
    }
}