using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Activities;
using GF.DillyDally.WriteModel.Domain.Activities.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class ActivityTests
    {
        #region SetupDatabaseAsync/Teardown

        [SetUp]
        public async Task Setup()
        {
            await this._testInfrastructure.SetupDatabaseAsync(UnittestsSetup.GetTestRunDatabaseName());
        }

        [TearDown]
        public void Destroy()
        {
            this._testInfrastructure.Destroy();
        }

        #endregion

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();

        [Test]
        public async Task Creating_Activity_ShouldCreateProjection()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var activityService = this._testInfrastructure.DiContainer.GetInstance<ActivityService>();
                var repository = new ActivityRepository();
                var activityName = this._testInfrastructure.TestData.GetRandomActivityName();

                // Act
                var newActivity = await activityService.CreatePercentageActivityAsync(activityName);
                var projection = await repository.GetByIdAsync(connection, newActivity.ActivityId);

                // Assert
                Assert.That(newActivity.ActivityId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(projection, Is.Not.Null);
                Assert.That(projection.ActivityId, Is.EqualTo(newActivity.ActivityId));
                Assert.That(projection.Name, Is.EqualTo(activityName));
            }
        }
    }
}