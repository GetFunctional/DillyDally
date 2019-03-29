using System;
using System.IO;
using System.Linq;
using GF.DillyDally.Data.Contracts;
using NUnit.Framework;

namespace GF.DillyDally.Data.Sqlite.Tests
{
    public class CreateDatabaseTests
    {
        [Test]
        public void Database_Create_GetsCreated()
        {
            // Arrange
            var databaseFileHandler = new DatabaseFileHandler();
            var exampleFile = "DillyDallyData.db";

            // Act && Assert
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase(exampleFile));

            Assert.That(File.Exists(fullexampleFile));
            databaseFileHandler.DeleteDatabase(exampleFile);
        }

        [Test]
        public void Database_CreateWithIncorrectNaming_ThrowsException()
        {
            // Arrange
            var databaseFileHandler = new DatabaseFileHandler();

            // Act && Assert
            Assert.Throws<ArgumentException>(() => databaseFileHandler.CreateNewDatabase(""));
            Assert.Throws<ArgumentException>(() => databaseFileHandler.CreateNewDatabase("   "));
            Assert.Throws<ArgumentException>(() => databaseFileHandler.CreateNewDatabase(null));
            Assert.Throws<ArgumentException>(() => databaseFileHandler.CreateNewDatabase(string.Join("", Enumerable.Repeat("a", 256).Select(x => x))));
        }

        [Test]
        public void Database_Delete_GetsDeleted()
        {
            // Arrange
            var databaseFileHandler = new DatabaseFileHandler();
            var exampleFile = "DillyDallyData.db";

            // Act
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase(exampleFile));
            Assert.That(File.Exists(fullexampleFile));
            databaseFileHandler.DeleteDatabase(exampleFile);

            // Assert
            Assert.That(!File.Exists(fullexampleFile));
        }
    }
}