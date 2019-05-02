using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class DatabaseFileHandler
    {
        private readonly string _databaseName;
        private readonly string _fullDatabaseFilePath;

        public DatabaseFileHandler(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName) || string.IsNullOrWhiteSpace(databaseName) ||
                databaseName.Length > 255)
            {
                throw new ArgumentException("Please provider a database name with Length > 0 and Length < 256");
            }

            this._databaseName = databaseName;
            this._fullDatabaseFilePath = this.GetFullDatabaseFilePath(databaseName);
        }

        public IGuidGenerator GuidGenerator { get; } = new GuidGenerator();

        public bool DatabaseExists()
        {
            return File.Exists(this._fullDatabaseFilePath);
        }

        public void CreateNewDatabase()
        {
            if (!this.DatabaseExists())
            {
                if (!Directory.Exists(Directories.GetUserApplicationDatabasesDirectory()))
                {
                    Directory.CreateDirectory(Directories.GetUserApplicationDatabasesDirectory());
                }

                this.CreateDillyDallyDatabase(this._fullDatabaseFilePath);
            }
        }

        public void DeleteDatabaseIfExists()
        {
            var fullDatabaseFilePath = this.GetFullDatabaseFilePath(this._databaseName);
            if (File.Exists(fullDatabaseFilePath))
            {
                File.Delete(fullDatabaseFilePath);
            }
        }

        private string GetFullDatabaseFilePath(string databaseName)
        {
            var databaseFile = this.GetDatabaseFileName(databaseName);
            var fullDatabaseFilePath = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), databaseFile);
            return fullDatabaseFilePath;
        }

        private string GetDatabaseFileName(string databaseName)
        {
            return !databaseName.EndsWith(".db") ? string.Concat(databaseName, ".db") : databaseName;
        }

        private void CreateDillyDallyDatabase(string fullDatabaseFilePath)
        {
            SQLiteConnection.CreateFile(fullDatabaseFilePath);
        }

        private string BuildConnectionString(string fullDatabaseFilePath)
        {
            var builder = new SQLiteConnectionStringBuilder
                          {
                              DataSource = fullDatabaseFilePath,
                              Version = 3,
                              BinaryGUID = true
                          };

            return builder.ConnectionString;
        }

        private SQLiteConnection CreateConnection(string fullDatabaseFilePath)
        {
            var connection = new SQLiteConnection(this.BuildConnectionString(fullDatabaseFilePath));
            return connection;
        }

        public IDbConnection OpenConnection()
        {
            var connection = this.CreateConnection(this._fullDatabaseFilePath);
            return connection.OpenAndReturn();
        }

        public async Task<IDbConnection> OpenConnectionAsync()
        {
            var connection = this.CreateConnection(this._fullDatabaseFilePath);
            await connection.OpenAsync();
            return connection;
        }

        public string GetConnectionString()
        {
            return this.BuildConnectionString(this._fullDatabaseFilePath);
        }
    }
}