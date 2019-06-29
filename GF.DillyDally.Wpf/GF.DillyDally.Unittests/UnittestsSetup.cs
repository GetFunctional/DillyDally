using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Unittests.Core;
using NUnit.Framework;

namespace GF.DillyDally.Unittests
{
    [SetUpFixture]
    public class UnittestsSetup
    {
        //private DatabaseTestSetup _databaseTestSetup;
        //private string _testRunName;

        public static string GetTestRunDatabaseName()
        {
            return $"TestRun_{Guid.NewGuid()}";
        }

        //[OneTimeSetUp]
        //public async Task RunBeforeAnyTests()
        //{
        //    //this._testRunName = GetTestRunDatabaseName();
        //    //DeleteUnittestDatabase(this._testRunName);
        //    //this._databaseTestSetup = new DatabaseTestSetup();
        //    //await this._databaseTestSetup.SetupAsync(this._testRunName);
        //}

        private static void DeleteUnittestDatabase(string databaseName)
        {
            //var fileHandler = new DatabaseFileHandler(databaseName);
            //fileHandler.DeleteDatabaseIfExists();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            //var fileHandler = new DatabaseFileHandler(this._testRunName);
            //fileHandler.ArchiveDatabase("Unittests_LastRun.db");
        }
    }
}