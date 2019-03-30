using System;

namespace GF.DillyDally.Data.Sqlite
{
    public interface IGuidGenerator
    {
        Guid GenerateGuid();
    }
}