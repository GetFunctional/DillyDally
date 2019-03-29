using System;
using System.Data.SQLite;
using System.IO;
using GF.DillyDally.Data.Contracts;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class DatabaseFileHandler
    {
        public void CreateNewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName) || databaseName.Length > 255)
            {
                throw new ArgumentException("Please provider a database name with Length > 0 and Length < 256");
            }

            var databaseFile = !databaseName.EndsWith(".db") ? string.Concat(databaseName, ".db") : databaseName;
            var fullDatabaseFilePath = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), databaseFile);
            if (!File.Exists(fullDatabaseFilePath))
            {
                if (!Directory.Exists(Directories.GetUserApplicationDatabasesDirectory()))
                {
                    Directory.CreateDirectory(Directories.GetUserApplicationDatabasesDirectory());
                }

                this.CreateDillyDallyDatabase(fullDatabaseFilePath);
            }
        }

        public void DeleteDatabase(string databaseName)
        {
            var databaseFile = !databaseName.EndsWith(".db") ? string.Concat(databaseName, ".db") : databaseName;
            var fullDatabaseFilePath = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), databaseFile);
            if (File.Exists(fullDatabaseFilePath))
            {
                File.Delete(fullDatabaseFilePath);
            }
        }

        private void CreateDillyDallyDatabase(string fullDatabaseFilePath)
        {
            SQLiteConnection.CreateFile(fullDatabaseFilePath);
        }
    }
}