using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Activities;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class ActivityTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.Run(UnittestsSetup.ExampleDatabase);
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

                var activityName = await this._testInfrastructure.TestData.GetRandomActivityNameAsync(activityService);
                var testImage = this._testInfrastructure.TestData.GetRandomImageBytes();

                // Act
                var newActivity = await activityService.CreatePercentageActivityAsync(activityName, testImage);
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