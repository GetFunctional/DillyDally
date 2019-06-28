using System;
using System.IO;

namespace GF.DillyDally.Data.Sqlite
{
    public static class DataDirectories
    {
        public static string GetUserApplicationDataDirectory()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return Path.Combine(path, "DillyDally");
        }

        public static string GetUserApplicationDatabasesDirectory()
        {
            return Path.Combine(GetUserApplicationDataDirectory(), "Data");
        }
    }
}