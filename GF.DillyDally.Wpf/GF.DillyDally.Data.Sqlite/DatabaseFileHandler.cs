using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using GF.DillyDally.Data.Contracts;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class DatabaseFileHandler
    {
        public void CreateNewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName) ||
                databaseName.Length > 255)
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
            var fullDatabaseFilePath = GetFullDatabaseFilePath(databaseName);
            if (File.Exists(fullDatabaseFilePath))
            {
                File.Delete(fullDatabaseFilePath);
            }
        }

        private static string GetFullDatabaseFilePath(string databaseName)
        {
            var databaseFile = !databaseName.EndsWith(".db") ? string.Concat(databaseName, ".db") : databaseName;
            var fullDatabaseFilePath = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), databaseFile);
            return fullDatabaseFilePath;
        }

        private void CreateDillyDallyDatabase(string fullDatabaseFilePath)
        {
            SQLiteConnection.CreateFile(fullDatabaseFilePath);
        }

        private string BuildConnectionString(string databaseFileName)
        {
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = databaseFileName,
                Version = 3,
                BinaryGUID = true
            };

            return builder.ConnectionString;
        }

        private IDbConnection CreateConnection(string databaseName)
        {
            var connection = new SQLiteConnection(this.BuildConnectionString(GetFullDatabaseFilePath(databaseName)));
            return connection.OpenAndReturn();
        }

        public IDbConnection OpenConnection(string databaseName)
        {
            return this.CreateConnection(databaseName);
        }
    }
}