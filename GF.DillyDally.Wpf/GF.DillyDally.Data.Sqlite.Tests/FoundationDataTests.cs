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
            var databaseFileHandler = new DatabaseFileHandler();
            var databaseUpdater = new DatabaseUpdater(new SqlScriptSelector());
            this._exampleFile = "FoundationDataTests.db";
            var exampleFile = this._exampleFile;
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

        [Test]
        public void FoundationData_Insert_AllDataIsPresent()
        {
            // Arrange
            var foundationDataProvider = new FoundationDataProvider();
            var databaseFileHandler = new DatabaseFileHandler();

            var entityInsertSuccessful = false;
            using (var connection = databaseFileHandler.OpenConnection(this._exampleFile))
            {
                foundationDataProvider.InsertBaseDataIntoDatabase(connection);

                var currencies = connection.GetAll<CurrencyEntity>().ToList();
                var balances = connection.GetAll<AccountBalanceEntity>().ToList();
                var rewardTemplates = connection.GetAll<RewardTemplateEntity>().ToList();
                entityInsertSuccessful = currencies.Count() == 6 && balances.Count() == currencies.Count() && rewardTemplates.Count() == 19;
            }

            Assert.That(entityInsertSuccessful, Is.True);
        }
    }
}