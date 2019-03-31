using System;
using System.Data;
using Dapper;

namespace GF.DillyDally.Data.Sqlite
{
    public class DatabaseUpdater
    {
        private readonly SqlScriptSelector _sqlScriptSelector;
        private readonly DatabaseFileHandler _databaseFileHandler;

        public DatabaseUpdater(SqlScriptSelector sqlScriptSelector, DatabaseFileHandler databaseFileHandler)
        {
            this._sqlScriptSelector = sqlScriptSelector;
            this._databaseFileHandler = databaseFileHandler;
        }

        public void UpdateDatabase()
        {
            using (var connection = this._databaseFileHandler.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var scriptsToRun =
                        this._sqlScriptSelector.GetUpdateStepsBeginningFromVersion(new Version(1, 0, 0, 0));
                    foreach (var updateStep in scriptsToRun)
                    {
                        foreach (var updateStepSqlCommand in updateStep.SqlCommands)
                        {
                            connection.Execute(updateStepSqlCommand);
                        }
                    }

                    transaction.Commit();
                }
            }
        }
    }
}