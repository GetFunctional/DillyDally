using System;
using System.Collections.Generic;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class UpdateStep
    {
        public UpdateStep(Version version, IList<string> sqlCommands)
        {
            this.Version = version;
            this.SqlCommands = sqlCommands;
        }

        public Version Version { get; }
        public IList<string> SqlCommands { get; }
    }
}