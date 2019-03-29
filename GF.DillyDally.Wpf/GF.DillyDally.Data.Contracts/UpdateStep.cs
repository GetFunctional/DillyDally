using System;
using System.Collections.Generic;

namespace GF.DillyDally.Data.Contracts
{
    public sealed class UpdateStep
    {
        public Version Version { get; }
        public IList<string> SqlCommands { get; }

        public UpdateStep(Version version, IList<string> sqlCommands)
        {
            this.Version = version;
            this.SqlCommands = sqlCommands;
        }
    }
}