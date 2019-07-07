using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Unittests.Core;
using NUnit.Framework;

namespace GF.DillyDally.Unittests
{
    [SetUpFixture]
    public class UnittestsSetup
    {
        public const string ExampleDatabase = "Unittests.db";

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            DeleteUnittestDatabase(ExampleDatabase);
            var infrastructure = new TestInfrastructure();
            await infrastructure.CreateNewUnittestDatabaseAsync(ExampleDatabase);
        }

        private static void DeleteUnittestDatabase(string databaseName)
        {
            var fileHandler = new DatabaseFileHandler(databaseName);
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