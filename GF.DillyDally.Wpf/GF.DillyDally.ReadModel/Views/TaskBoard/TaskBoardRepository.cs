using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    public sealed class TaskBoardRepository
    {
        public async Task<IList<TaskBoardLaneEntity>> GetTaskBoardLanesAsync(IDbConnection connection)
        {
            var sql = $"SELECT {nameof(TaskBoardLaneEntity.LaneId)}, {nameof(TaskBoardLaneEntity.Name)} " +
                      $"FROM {LaneEntity.TableNameConstant} " +
                      $"WHERE {nameof(LaneEntity.IsCompletedLane)} = 0 AND {nameof(LaneEntity.IsRejectedLane)} = 0;" +
                      $"SELECT {nameof(TaskBoardTaskEntity.TaskId)}, {TaskEntity.TableNameConstant}.{nameof(TaskBoardTaskEntity.LaneId)}, {nameof(TaskBoardTaskEntity.CategoryId)}, {nameof(TaskBoardTaskEntity.RunningNumber)}, {TaskEntity.TableNameConstant}.{nameof(TaskBoardTaskEntity.RunningNumberId)}, {TaskEntity.TableNameConstant}.{nameof(TaskBoardTaskEntity.Name)} " +
                      $"FROM {TaskEntity.TableNameConstant} " +
                      $"JOIN {RunningNumberEntity.TableNameConstant} ON {RunningNumberEntity.TableNameConstant}.{nameof(RunningNumberEntity.RunningNumberId)} = {TaskEntity.TableNameConstant}.{nameof(TaskEntity.RunningNumberId)} " +
                      $"JOIN {LaneEntity.TableNameConstant} ON {LaneEntity.TableNameConstant}.{nameof(LaneEntity.LaneId)} = {TaskEntity.TableNameConstant}.{nameof(TaskEntity.LaneId)} " +
                      $"WHERE {nameof(LaneEntity.IsCompletedLane)} = 0 AND {nameof(LaneEntity.IsRejectedLane)} = 0;";

            using (var multiSelect = await connection.QueryMultipleAsync(sql))
            {
                var lanes= (await multiSelect.ReadAsync<TaskBoardLaneEntity>()).ToList();
                var tasks = await multiSelect.ReadAsync<TaskBoardTaskEntity>();

                foreach (var taskBoardLaneEntity in lanes)
                {
                    taskBoardLaneEntity.Tasks = new List<TaskBoardTaskEntity>(tasks.Where(x => x.LaneId == taskBoardLaneEntity.LaneId));
                }

                return lanes;
            }
        }
    }
}