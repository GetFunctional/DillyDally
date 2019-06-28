using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using log4net;

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
                throw new ArgumentException("Please provide a database name with Length > 0 and Length < 256");
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
                if (!Directory.Exists(DataDirectories.GetUserApplicationDatabasesDirectory()))
                {
                    Directory.CreateDirectory(DataDirectories.GetUserApplicationDatabasesDirectory());
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
            var fullDatabaseFilePath = Path.Combine(DataDirectories.GetUserApplicationDatabasesDirectory(), databaseFile);
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
                              BinaryGUID = true,
                              DefaultTimeout = 5000,
                              SyncMode = SynchronizationModes.Off,
                              JournalMode = SQLiteJournalModeEnum.Memory,
                              PageSize = 65536,
                              CacheSize = 16777216,
                              FailIfMissing = false,
                              ReadOnly = false
                          };

            return builder.ConnectionString;
        }

        private SQLiteConnection CreateConnection(string fullDatabaseFilePath)
        {
            var connection = new SQLiteConnection(this.BuildConnectionString(fullDatabaseFilePath));
            return connection;
        }

        private static readonly ILog DatabaseFileLogger = LogManager.GetLogger(typeof(DatabaseFileHandler));

        private void HandleTrace(object sender, TraceEventArgs e)
        {
            DatabaseFileLogger.Info(e.Statement);
        }

        public IDbConnection OpenConnection()
        {
            var connection = this.CreateConnection(this._fullDatabaseFilePath).OpenAndReturn();
            connection.Trace += this.HandleTrace;
            return connection;
        }

        public async Task<IDbConnection> OpenConnectionAsync()
        {
            var connection = this.CreateConnection(this._fullDatabaseFilePath);
            await connection.OpenAsync();
            connection.Trace += this.HandleTrace;
            return connection;
        }

        public string GetConnectionString()
        {
            return this.BuildConnectionString(this._fullDatabaseFilePath);
        }

        public void ArchiveDatabase(string archiveName)
        {
            var currentDatabase = this.GetFullDatabaseFilePath(this._databaseName);
            var archiveDatabase = this.GetFullDatabaseFilePath(archiveName);

            if (File.Exists(archiveDatabase))
            {
                File.Delete(archiveDatabase);
            }

            File.Move(currentDatabase, archiveDatabase);
        }
    }
}