using System.Threading.Tasks;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Activities;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
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
        public async Task Creating_Activity_PersistsActivity()
        {
            // Arrange
            var activityService = this._testInfrastructure.DiContainer.GetInstance<ActivityService>();
            var aggregateRepository = this._testInfrastructure.DiContainer.GetInstance<IAggregateRepository>();
            var activityName = await this._testInfrastructure.TestData.GetRandomActivityNameAsync(activityService);

            // Act
            var activity = await activityService.CreatePercentageActivityAsync(activityName);

            // Assert
            var aggregate = aggregateRepository.GetById<ActivityAggregateRoot>(activity.ActivityId);
            Assert.That(aggregate != null, Is.True);
            Assert.That(aggregate.Name, Is.EqualTo(activityName));
        }

        [Test]
        public async Task Creating_ActivityWithImage_PersistsActivity()
        {
            // Arrange
            var activityService = this._testInfrastructure.DiContainer.GetInstance<ActivityService>();
            var aggregateRepository = this._testInfrastructure.DiContainer.GetInstance<IAggregateRepository>();
            var activityName = await this._testInfrastructure.TestData.GetRandomActivityNameAsync(activityService);
            var testImageBytes = this._testInfrastructure.TestData.GetRandomImageBytes();

            // Act
            var activity = await activityService.CreatePercentageActivityAsync(activityName, testImageBytes);

            // Assert
            var aggregate = aggregateRepository.GetById<ActivityAggregateRoot>(activity.ActivityId);
            Assert.That(aggregate != null, Is.True);
            Assert.That(aggregate.Name, Is.EqualTo(activityName));
            Assert.That(aggregate.PreviewImageFileId, Is.Not.Null);
        }
    }
}