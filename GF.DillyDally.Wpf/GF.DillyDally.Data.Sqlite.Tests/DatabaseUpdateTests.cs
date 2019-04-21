using System.IO;
using GF.DillyDally.Data.Contracts;
using NUnit.Framework;

namespace GF.DillyDally.Data.Sqlite.Tests
{
    [TestFixture]
    public class DatabaseUpdateTests
    {
        [Test]
        public void Database_CreateAndUpdate_UpdatesRunWithoutException()
        {
            // Arrange
            var exampleFile = "UpdateTest.db";
            var databaseFileHandler = new DatabaseFileHandler(exampleFile);
            var databaseUpdater = new DatabaseUpdater(new SqlScriptSelector(), databaseFileHandler);
            databaseFileHandler.DeleteDatabaseIfExists();

            // Act && Assert
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase());
            var fileExists = File.Exists(fullexampleFile);
            Assert.DoesNotThrow(() => databaseUpdater.UpdateDatabase());

            Assert.That(fileExists);
        }
    }
}