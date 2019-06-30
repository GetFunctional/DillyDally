using System.Threading.Tasks;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.Unittests.Mvvmc.TestModels;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.Mvvmc
{
    [TestFixture]
    internal class ControllerTests
    {
        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.Run( UnittestsSetup.ExampleDatabase);
            this._testInfrastructure.DiContainer.Register<TestController>();
            this._testInfrastructure.DiContainer.Register<TestViewModel>();
            this._testInfrastructure.DiContainer.Register<ChildTestController>();
            this._testInfrastructure.DiContainer.Register<ChildViewModel>();
        }
        

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();

        [Test]
        public async Task Controller_Initialize_Hierarchical()
        {
            // Arrange
            var controllerFactory = this._testInfrastructure.GetControllerFactory();
            var testController = controllerFactory.CreateController<TestController>();

            // Act
            await testController.InitializeAsync();

            // Assert
            var childController = testController.ChildController;
            Assert.That(childController.IsInitialized, Is.EqualTo(true));
        }

        [Test]
        public async Task Controller_ChildControllerGetsInitialized_EvenAfterInitialization()
        {
            // Arrange
            var controllerFactory = this._testInfrastructure.GetControllerFactory();
            var testController = controllerFactory.CreateController<TestController>();

            // Act
            await testController.InitializeAsync();
            testController.CreateAnotherChildController();

            // Assert
            Assert.That(testController.AnotherChildController.IsInitialized, Is.EqualTo(true));
        }

        [Test]
        public async Task Controller_Initialize_TriggersOnlyOnce()
        {
            // Arrange
            var controllerFactory = this._testInfrastructure.GetControllerFactory();
            var testController = controllerFactory.CreateController<TestController>();

            // Act
            await testController.InitializeAsync();
            await testController.InitializeAsync();
            await testController.InitializeAsync();

            // Assert
            Assert.That(testController.InitializationCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Controller_Initialize_TriggersOnlyOnceEvenWithparallelExecution()
        {
            // Arrange
            var controllerFactory = this._testInfrastructure.GetControllerFactory();
            var testController = controllerFactory.CreateController<TestController>();

            // Act
#pragma warning disable 4014
            testController.InitializeAsync();
#pragma warning restore 4014
#pragma warning disable 4014
            testController.InitializeAsync();
#pragma warning restore 4014
            await testController.InitializeAsync();

            // Assert
            Assert.That(testController.InitializationCount, Is.EqualTo(1));
        }
    }
}