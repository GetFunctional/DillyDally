using System;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class GuidGenerator : IGuidGenerator
    {
        #region IGuidGenerator Members

        public Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        #endregion
    }
}