using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Sqlite
{
    public class DatabaseUpdater
    {
        private readonly SqlScriptSelector _sqlScriptSelector;

        public DatabaseUpdater(SqlScriptSelector sqlScriptSelector)
        {
            this._sqlScriptSelector = sqlScriptSelector;
        }
    }
}
