using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Unittests.Core;
using NUnit.Framework;

namespace GF.DillyDally.Unittests
{
    [TestFixture]
    public class DatabaseUpdateTests
    {
        [Test]
        public async Task Database_CreateAndUpdate_UpdatesRunWithoutException()
        {
            // Arrange
            var exampleFile = "UpdateTest.db";
            var databaseFileHandler = new DatabaseFileHandler(exampleFile);
            databaseFileHandler.DeleteDatabaseIfExists();
            var testInfrastructure = new TestInfrastructure();

            // Act && Assert

            await testInfrastructure.CreateNewUnittestDatabaseAsync(exampleFile);
            databaseFileHandler.DeleteDatabaseIfExists();
        }
    }
}