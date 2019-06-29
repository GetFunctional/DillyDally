using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.Unittests.Core;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using LightInject;
using MediatR;
using NUnit.Framework;

namespace GF.DillyDally.Unittests.ReadModel.Projection
{
    [TestFixture]
    public class CategoryTests
    {
        #region SetupDatabase/Teardown

        [SetUp]
        public void Setup()
        {
            this._testInfrastructure.SetupDatabase(UnittestsSetup.ExampleDatabase);
        }

        #endregion

        private readonly TestInfrastructure _testInfrastructure = new TestInfrastructure();

        [Test]
        public async Task Creating_Category_ShouldCreateProjection()
        {
            using (var connection = this._testInfrastructure.OpenDatabaseConnection())
            {
                // Arrange
                var commandDispatcher = this._testInfrastructure.DiContainer.GetInstance<IMediator>();
                var categoryRepository = new CategoryRepository();

                // Act
                var categoryResponse = await commandDispatcher.Send(new CreateCategoryCommand("Test", "#123456"));
                var categoryFromProjection = await categoryRepository.GetByIdAsync(connection, categoryResponse.CategoryId);

                // Assert
                Assert.That(categoryResponse, Is.Not.EqualTo(Guid.Empty));
                Assert.That(categoryFromProjection, Is.Not.Null);
            }
        }
    }
}