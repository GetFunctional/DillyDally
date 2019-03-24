using System;
using System.Data.SQLite;
using System.IO;
using GF.DillyDally.Contracts;
using GF.DillyDally.Data.Account;
using GF.DillyDally.Data.Common;
using GF.DillyDally.Data.Tasks;

namespace GF.DillyDally.Data
{
    public sealed class DataInitializer
    {
        private const string DefaultDatabaseName = "Data.db";

        public void InitializeDataLayer(Action<Type, Type> serviceRegister)
        {
            RegisterTypes(serviceRegister);

            this.CreateOrUpdateDatabase();
        }

        private static void RegisterTypes(Action<Type, Type> serviceRegister)
        {
            serviceRegister(typeof(ITasksRepository), typeof(TasksRepository));
            serviceRegister(typeof(ICommonDataRepository), typeof(CommonDataRepository));
            serviceRegister(typeof(IAccountRepository), typeof(AccountRepository));

        }

        private void CreateOrUpdateDatabase()
        {
            var databaseFile = Path.Combine(Directories.GetUserApplicationDatabasesDirectory(), DefaultDatabaseName);
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
            SQLiteConnection.CreateFile(databaseFile);

            using (var connection =
                new SQLiteConnection($"Data Source={DefaultDatabaseName};Version=3;"))
            {
                connection.Open();


                //string sql = "create table highscores (name varchar(20), score int)";

                //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                //command.ExecuteNonQuery();

                //sql = "insert into highscores (name, score) values ('Me', 9001)";

                //command = new SQLiteCommand(sql, m_dbConnection);
                //command.ExecuteNonQuery();
            }
        }
    }
}