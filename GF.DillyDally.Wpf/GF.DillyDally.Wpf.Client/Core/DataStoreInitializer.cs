using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal class DataStoreInitializer
    {
        public DatabaseFileHandler Initialize(string defaultDatabaseName)
        {
            return this.InitializeDatabase(defaultDatabaseName);
        }

        private DatabaseFileHandler InitializeDatabase(string defaultDatabaseName)
        {
            var databaseFileHandler = new DatabaseFileHandler(defaultDatabaseName);
            if (!databaseFileHandler.DatabaseExists())
            {
                databaseFileHandler.CreateNewDatabase();
                var databaseUpdater = new DatabaseUpdater(new SqlScriptSelector(), databaseFileHandler);
                databaseUpdater.UpdateDatabase();
                var baseDataFiller = new FoundationDataProvider(databaseFileHandler);
                baseDataFiller.InsertBaseDataIntoDatabase();
            }

            return databaseFileHandler;
        }
    }
}