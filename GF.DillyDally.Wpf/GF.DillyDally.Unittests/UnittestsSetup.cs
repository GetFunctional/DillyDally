using System.Threading.Tasks;
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
        public async Task RunBeforeAnyTests()
        {
            DeleteUnittestDatabase();
            this._databaseTestSetup = new DatabaseTestSetup();
            await this._databaseTestSetup.SetupAsync(ExampleDatabase);
        }

        private static void DeleteUnittestDatabase()
        {
            var fileHandler = new DatabaseFileHandler(ExampleDatabase);
            fileHandler.DeleteDatabaseIfExists();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            var fileHandler = new DatabaseFileHandler(ExampleDatabase);
            fileHandler.ArchiveDatabase("Unittests_LastRun.db");
        }
    }
}