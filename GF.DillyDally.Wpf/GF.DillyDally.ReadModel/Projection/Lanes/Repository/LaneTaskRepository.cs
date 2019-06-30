using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.ReadModel.Projection.Lanes.Repository
{
    internal sealed class LaneTaskRepository : Repository<LaneTaskEntity>
    {
        public async Task AddTaskToLaneAsync(IDbConnection connection, Guid taskId, Guid laneId, int orderNumber)
        {
            await this.InsertAsync(connection, new LaneTaskEntity
                                               {
                                                   LaneTaskId = this.GuidGenerator.GenerateGuid(),
                                                   TaskId = taskId,
                                                   LaneId = laneId,
                                                   OrderNumber = orderNumber
                                               });
        }

        public async Task RemoveTaskFromLaneAsync(IDbConnection connection, Guid taskId, Guid laneId)
        {
            var querySql =
                "DELETE " +
                $"FROM {LaneTaskEntity.TableNameConstant} " +
                $"WHERE {nameof(LaneTaskEntity.TaskId)} = @taskId AND {nameof(LaneTaskEntity.LaneId)} = @laneId;";
            await connection.ExecuteAsync(querySql, new {taskId, laneId});
        }

        public async Task<LaneTaskEntity> GetLaneTaskByTaskId(IDbConnection connection, Guid taskId)
        {
            var querySql =
                "SELECT * " +
                $"FROM {LaneTaskEntity.TableNameConstant} " +
                $"WHERE {nameof(LaneTaskEntity.TaskId)} = @taskId;";
            return (await connection.QueryAsync<LaneTaskEntity>(querySql, new {taskId})).SingleOrDefault();
        }
    }
}