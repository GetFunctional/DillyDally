using System.IO;
using System.Linq;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts;
using GF.DillyDally.Data.Sqlite.Entities;
using NUnit.Framework;

namespace GF.DillyDally.Data.Sqlite.Tests
{
    [TestFixture]
    public class FoundationDataTests
    {
        private string _exampleFile;

        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange
            this._exampleFile = "FoundationDataTests.db";
            var exampleFile = this._exampleFile;
            var databaseFileHandler = new DatabaseFileHandler(exampleFile);
            var databaseUpdater = new DatabaseUpdater(new SqlScriptSelector(), databaseFileHandler);
            databaseFileHandler.DeleteDatabase();

            // Act && Assert
            var fullexampleFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), exampleFile);
            Assert.DoesNotThrow(() => databaseFileHandler.CreateNewDatabase());
            var fileExists = File.Exists(fullexampleFile);
            Assert.DoesNotThrow(() => databaseUpdater.UpdateDatabase());

            Assert.That(fileExists);
        }

        [Test]
        public void FoundationData_Insert_AllDataIsPresent()
        {
            // Arrange
            var databaseFileHandler = new DatabaseFileHandler(this._exampleFile);
            var foundationDataProvider = new FoundationDataProvider(databaseFileHandler);

            var entityInsertSuccessful = false;
            foundationDataProvider.InsertBaseDataIntoDatabase();

            using (var connection = databaseFileHandler.OpenConnection())
            {
                var currencies = connection.GetAll<CurrencyEntity>().ToList();
                var balances = connection.GetAll<AccountBalanceEntity>().ToList();
                var rewardTemplates = connection.GetAll<RewardTemplateEntity>().ToList();
                entityInsertSuccessful = currencies.Count() == 6 && balances.Count() == currencies.Count() &&
                                         rewardTemplates.Count() == 19;
            }

            Assert.That(entityInsertSuccessful, Is.True);
        }
    }
}