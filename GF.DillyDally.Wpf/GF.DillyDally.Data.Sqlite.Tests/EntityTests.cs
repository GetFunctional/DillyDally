using System.IO;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts;
using GF.DillyDally.Data.Sqlite.Entities;
using NUnit.Framework;

namespace GF.DillyDally.Data.Sqlite.Tests
{
    [TestFixture]
    public class EntityTests
    {
        private string _exampleFile;

        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange
            this._exampleFile = "EntityTests.db";
            var exampleFile = this._exampleFile;
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

        [Test]
        public void Currency_Insert_InsertsRecord()
        {
            // Arrange
            var databaseFileHandler = new DatabaseFileHandler(this._exampleFile);
            var entityFactory = new EntityFactory();

            var entityInsertSuccessful = false;
            using (var connection = databaseFileHandler.OpenConnection())
            {
                var currency = entityFactory.CreateCurrencyEntity("Test1", "T");
                var currencyId = currency.CurrencyKey;
                connection.Insert(currency);

                var readFromDb = connection.Get<CurrencyEntity>(currencyId.CurrencyId);
                entityInsertSuccessful = readFromDb.CurrencyKey == currency.CurrencyKey && currency.Name == "Test1" &&
                                         currency.Code == "T";
            }

            Assert.That(entityInsertSuccessful, Is.True);
        }
    }
}