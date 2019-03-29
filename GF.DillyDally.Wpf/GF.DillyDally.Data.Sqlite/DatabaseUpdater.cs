using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GF.DillyDally.Data.Sqlite
{
    public class DatabaseUpdater
    {
        private readonly SqlScriptSelector _sqlScriptSelector;

        public DatabaseUpdater(SqlScriptSelector sqlScriptSelector)
        {
            this._sqlScriptSelector = sqlScriptSelector;
        }

        public void UpdateDatabase(IDbConnection connection)
        {
            var scriptsToRun = this._sqlScriptSelector.GetUpdateStepsBeginningFromVersion(new Version(1, 0, 0, 0));
            foreach (var updateStep in scriptsToRun)
            {
                foreach (var updateStepSqlCommand in updateStep.SqlCommands)
                {
                    connection.Execute(updateStepSqlCommand);
                }
            }
        }
    }
}
