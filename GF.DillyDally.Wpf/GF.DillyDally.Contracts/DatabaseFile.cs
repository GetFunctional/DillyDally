using System.IO;

namespace GF.DillyDally.Contracts
{
    public class DatabaseFile
    {
        private const string DefaultDatabaseName = "Data.db";


        public static string GetDefault()
        {
            var databaseFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), DefaultDatabaseName);
            return databaseFile;
        }
    }
}