using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using GF.DillyDally.Contracts;

namespace GF.DillyDally.WriteModel
{
    internal sealed class StoreConnection
    {
        internal static IDbConnection CreateConnection()
        {
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = DatabaseFile.GetDefault(),
                Version = 3,
                BinaryGUID = true
            };

            var connection = new SQLiteConnection(builder.ConnectionString);
            connection.Trace+= HandleConnectionTrace;
            return connection.OpenAndReturn();
        }

        private static void HandleConnectionTrace(object sender, TraceEventArgs e)
        {
            Trace.Write(e.Statement);
        }
    }
}