using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class CategoryTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._infrastructureSetup.Setup(UnittestsSetup.ExampleDatabase);
        }

        #endregion

        private readonly InfrastructureTestSetup _infrastructureSetup = new InfrastructureTestSetup();

        [Test]
        public async Task Creating_Category_ShouldCreateProjection()
        {
            // Arrange
            var commandDispatcher = this._infrastructureSetup.DiContainer.GetInstance<IMediator>();
            var categoryRepository = this._infrastructureSetup.DiContainer.GetInstance<ICategoryRepository>();

            // Act
            var categoryResponse = await commandDispatcher.Send(new CreateCategoryCommand("Test", "#123456"));
            var categoryFromProjection = await categoryRepository.GetByIdAsync(categoryResponse.CategoryId);

            // Assert
            Assert.That(categoryResponse, Is.Not.EqualTo(Guid.Empty));
            Assert.That(categoryFromProjection, Is.Not.Null);
        }
    }
}