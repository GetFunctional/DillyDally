using System;
using System.Collections.Generic;

namespace GF.DillyDally.Update.UpdateSteps.SqlScripts
{
    public sealed class SqlScript
    {
        public SqlScript(Version version, IList<string> sqlCommands)
        {
            this.Version = version;
            this.SqlCommands = sqlCommands;
        }

        public Version Version { get; }
        public IList<string> SqlCommands { get; }
    }
}