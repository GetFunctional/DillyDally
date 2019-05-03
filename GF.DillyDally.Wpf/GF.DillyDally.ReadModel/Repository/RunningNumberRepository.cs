using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class RunningNumberRepository : Repository<RunningNumberEntity>, IRunningNumberRepository
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