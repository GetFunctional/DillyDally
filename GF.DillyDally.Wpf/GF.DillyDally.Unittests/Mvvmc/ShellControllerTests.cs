using GF.DillyDally.Unittests.Core;
using GF.DillyDally.Wpf.Client.Presentation;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.Mvvmc
{
    [TestFixture]
    internal class ShellControllerTests
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
        public void ShellController_CanBeCreated()
        {
            // Arrange
            var controllerFactory = this._testInfrastructure.GetControllerFactory();
            var testController = controllerFactory.CreateController<ShellController>();
            Assert.That(testController, Is.Not.Null);
        }
    }
}