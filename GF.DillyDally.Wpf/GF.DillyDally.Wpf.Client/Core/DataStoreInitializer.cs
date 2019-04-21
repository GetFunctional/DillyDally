﻿using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal class DataStoreInitializer
    {
        public DatabaseFileHandler Initialize(InitializationSettings settings)
        {
            return this.InitializeDatabase(settings);
        }

        private DatabaseFileHandler InitializeDatabase(InitializationSettings settings)
        {
            var databaseFileHandler = new DatabaseFileHandler(settings.DatabaseName);
            if (settings.EnforceRecreationOfDatabase || !databaseFileHandler.DatabaseExists())
            {
                databaseFileHandler.DeleteDatabaseIfExists();
                databaseFileHandler.CreateNewDatabase();
                var databaseUpdater = new DatabaseUpdater(new SqlScriptSelector(), databaseFileHandler);
                databaseUpdater.UpdateDatabase();
            }

            return databaseFileHandler;
        }
    }
}