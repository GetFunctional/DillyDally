using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;

namespace GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository
{
    internal class RunningNumberCounterRepository : Repository<RunningNumberCounterEntity>
    {
        public async Task CreateNewRunningNumberCounterAsync(IDbConnection connection, Guid runningNumberCounterId,
            RunningNumberCounterArea counterArea,
            int initialNumber, string prefix)
        {
            await connection.InsertAsync(new RunningNumberCounterEntity
            {
                RunningNumberCounterId = runningNumberCounterId,
                CurrentNumber = initialNumber,
                CounterArea = counterArea,
                Prefix = prefix
            });
        }

        public async Task IncreaseCounterAsync(IDbConnection connection, Guid runningNumberCounterId)
        {
            var sql = $"UPDATE {RunningNumberCounterEntity.TableNameConstant} " +
                      $"SET {nameof(RunningNumberCounterEntity.CurrentNumber)} = {nameof(RunningNumberCounterEntity.CurrentNumber)} + 1 " +
                      $"WHERE {nameof(RunningNumberCounterEntity.RunningNumberCounterId)} = @runningNumberCounterId;";

            await connection.ExecuteAsync(sql, new {runningNumberCounterId});
        }
    }
}