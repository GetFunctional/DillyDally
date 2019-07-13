using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.WriteModel
{
    [TestFixture]
    public class RunningNumberTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.Run(Unittests.ExampleDatabase);
        }

        #endregion

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();

        [TearDown]
        [Test]
        public async Task RunningNumber_GetNext_IncreasesCounter()
        {
            // Arrange
            var aggregateRepository = this._testInfrastructure.DiContainer.GetInstance<IAggregateRepository>();
            var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
            var nextNumberCommand = new CreateRunningNumberCommand(RunningNumberCounterArea.Achievement);

            var attachResult = await commandDispatcher.Send(nextNumberCommand);
            var runningNumberCounter =
                aggregateRepository.GetById<RunningNumberCounterAggregateRoot>(
                    RunningNumberCounterCommandHandler.AreaToIdentityMapping[RunningNumberCounterArea.Achievement]);
            var nextNumber =
                runningNumberCounter.Numbers.Single(x => x.RunningNumberId == attachResult.RunningNumberId);

            Assert.That(nextNumber, Is.Not.Null);
        }
    }
}