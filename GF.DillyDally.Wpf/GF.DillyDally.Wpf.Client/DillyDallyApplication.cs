using System;
using System.Data.SQLite;
using System.IO;
using Dapper;
using GF.DillyDally.Contracts;
using GF.DillyDally.ReadModel.Common;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation;

namespace GF.DillyDally.Wpf.Client
{
    internal sealed class DillyDallyApplication : IDillyDallyApplication
    {
        private readonly Shell _shell;

        private readonly ShellController _shellController;

        internal DillyDallyApplication(ShellController shellController, Shell shell)
        {
            this._shellController = shellController;
            this._shell = shell;
        }

        #region IDillyDallyApplication Members

        public bool NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget)
        {
            return this._shellController.NavigateInCurrentNavigatorTo(navigationTarget);
        }

        #endregion

        public void CreateOrUpdateDatabase()
        {
            var databaseFile = DatabaseFile.GetDefault();
            if (!File.Exists(databaseFile))
            {
                if (!Directory.Exists(Directories.GetUserApplicationDatabasesDirectory()))
                {
                    Directory.CreateDirectory(Directories.GetUserApplicationDatabasesDirectory());
                }

                this.CreateDillyDallyDatabase(databaseFile);
            }
        }


        private void CreateDillyDallyDatabase(string databaseFile)
        {
            var dbTemplateFile = Path.Combine(Environment.CurrentDirectory, "Resources/DatabaseTemplate/DbSchema.db");
            File.Copy(dbTemplateFile, databaseFile, true);

            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = databaseFile,
                Version = 3,
                BinaryGUID = true
            };

            using (var connection =
                new SQLiteConnection(builder.ConnectionString))
            {
                connection.Open();
                //connection.AddTypeMapping()

                //connection.
                var sql = new SQLiteCommand(
                    @"INSERT INTO Currency ( CurrencyKey, Name, Code ) VALUES ( @currencyId, @name, @code);",
                    connection);
                sql.Parameters.AddWithValue("@currencyId", Guid.NewGuid());
                sql.Parameters.AddWithValue("@name", "Test");
                sql.Parameters.AddWithValue("@code", "CodeTest");
                sql.ExecuteNonQuery();

                var read = @"SELECT CurrencyKey, Name, Code FROM Currency";
                var data = connection.Query<CurrencyEntity>(read);
                //string sql = "create table highscores (name varchar(20), score int)";

                //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                //command.ExecuteNonQuery();

                //sql = "insert into highscores (name, score) values ('Me', 9001)";

                //command = new SQLiteCommand(sql, m_dbConnection);
                //command.ExecuteNonQuery();
            }
        }

        public void ShowUi()
        {
            this._shell.ShowDialog();
        }
    }
}