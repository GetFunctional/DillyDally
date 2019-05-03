using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite.Repository.Base;

namespace GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository
{
    internal class RunningNumberRepository : Repository<RunningNumberEntity>
    {
        public async Task CreateNewRunningNumberAsync(IDbConnection connection, Guid runningNumberId, Guid runningNumberCounterId, string runningNumber)
        {
            var runningNumberEntity = new RunningNumberEntity
                                      {
                                          RunningNumberId = runningNumberId,
                                          RunningNumberCounterId = runningNumberCounterId,
                                          RunningNumber = runningNumber
                                      };

            await connection.InsertAsync(runningNumberEntity);
        }
    }
}