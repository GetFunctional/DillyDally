using System;
using System.Data;
using System.IO;
using System.Linq;
using GF.DillyDally.Data.Contracts;
using NUnit.Framework;

namespace GF.DillyDally.Data.Sqlite.Tests
{
    [TestFixture]
    public class CreateDatabaseTests
    {
        [Test]
        public void Database_Create_ConnectionCouldBeEstablished()
        {
            // Arrange
            var exampleFile = "ConnectionAfterCreateTest.db";
            var databaseFileHandler = new DatabaseFileHandler(exampleFile);
            databaseFileHandler.DeleteDatabase();

            // Act && Assert
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase());
            var fileWasCreated = File.Exists(fullexampleFile);
            var connectionWasOpened = false;
            using (var connection = databaseFileHandler.OpenConnection())
            {
                connectionWasOpened = connection.State == ConnectionState.Open;
            }

            databaseFileHandler.DeleteDatabase();


            Assert.That(fileWasCreated);
            Assert.That(connectionWasOpened);
        }

        [Test]
        public void Database_Create_GetsCreated()
        {
            // Arrange
            var exampleFile = "CreateFileTest.db";
            var databaseFileHandler = new DatabaseFileHandler(exampleFile);
            databaseFileHandler.DeleteDatabase();

            // Act && Assert
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase());

            Assert.That(File.Exists(fullexampleFile));
            databaseFileHandler.DeleteDatabase();
        }

        [Test]
        public void Database_CreateWithIncorrectNaming_ThrowsException()
        {
            // Act && Assert
            Assert.Throws<ArgumentException>(() => new DatabaseFileHandler(""));
            Assert.Throws<ArgumentException>(() => new DatabaseFileHandler("   "));
            Assert.Throws<ArgumentException>(() => new DatabaseFileHandler(null));
            Assert.Throws<ArgumentException>(() =>
                new DatabaseFileHandler(string.Join("", Enumerable.Repeat("a", 256).Select(x => x))));
        }

        [Test]
        public void Database_Delete_GetsDeleted()
        {
            // Arrange
            var exampleFile = "DeleteTest.db";
            var databaseFileHandler = new DatabaseFileHandler(exampleFile);
            databaseFileHandler.DeleteDatabase();

            // Act
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase());
            Assert.That(File.Exists(fullexampleFile));
            databaseFileHandler.DeleteDatabase();

            // Assert
            Assert.That(!File.Exists(fullexampleFile));
        }
    }
}