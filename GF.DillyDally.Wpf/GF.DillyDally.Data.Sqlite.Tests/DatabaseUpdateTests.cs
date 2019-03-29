using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var databaseFileHandler = new DatabaseFileHandler();
            var databaseUpdater = new DatabaseUpdater(new SqlScriptSelector());
            var exampleFile = "UpdateTest.db";
            databaseFileHandler.DeleteDatabase(exampleFile);

            // Act && Assert
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase(exampleFile));
            var fileExists = File.Exists(fullexampleFile);
            using (var connection = databaseFileHandler.OpenConnection(exampleFile))
            {
                Assert.DoesNotThrow(() => databaseUpdater.UpdateDatabase(connection));
            }

            Assert.That(fileExists);
        }
    }
}
