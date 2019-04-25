using GF.DillyDally.Data.Sqlite;
using NUnit.Framework;

namespace GF.DillyDally.Unittests
{
    [SetUpFixture]
    public class UnittestsSetup
    {
        public const string ExampleDatabase = "Unittests.db";
        private DatabaseTestSetup _databaseTestSetup;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            DeleteUnittestDatabase();
            this._databaseTestSetup = new DatabaseTestSetup();
            this._databaseTestSetup.Setup(ExampleDatabase);
        }

        private static void DeleteUnittestDatabase()
        {
            var fileHandler = new DatabaseFileHandler(ExampleDatabase);
            fileHandler.DeleteDatabaseIfExists();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            DeleteUnittestDatabase();
        }
    }
}