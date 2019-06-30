using System;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite;
using MediatR;

namespace GF.DillyDally.Update
{
    public sealed class DatabaseUpdater
    {
        private readonly UpdateCoordinator _updateCoordinator;

        public DatabaseUpdater(UpdateCoordinator updateCoordinator)
        {
            this._updateCoordinator = updateCoordinator;
        }

        public async Task UpdateDatabaseAsync(IMediator mediator, string database)
        {
            var databaseFileHandler = new DatabaseFileHandler(database);

            foreach (var updateStep in this._updateCoordinator.GetUpdateStepsBeginningFromVersion(new Version(1, 0, 0, 0)))
            {
                using (var connection = databaseFileHandler.OpenConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        var scriptsToRun = updateStep.GetSqlUpdateScripts();
                        foreach (var script in scriptsToRun)
                        {
                            await connection.ExecuteAsync(script);
                        }

                        transaction.Commit();
                    }
                }

                await updateStep.PerformMigrationsAsync(mediator);
            }
        }
    }
}