using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    public sealed class TaskBoardRepository
    {
        public async Task<IList<TaskBoardLaneEntity>> GetTaskBoardLanesAsync(IDbConnection connection)
        {
            var tasksOnBoard = new List<TaskBoardLaneEntity>();

            var sql = $"SELECT {nameof(TaskBoardLaneEntity.LaneId)}, {nameof(TaskBoardLaneEntity.LaneName)} " +
                      $"FROM {LaneEntity.TableNameConstant} " +
                      $"WHERE {nameof(LaneEntity.IsCompletedLane)} = 0 AND {nameof(LaneEntity.IsRejectedLane)} = 0;" +
                      $"SELECT {nameof(TaskBoardTaskEntity.TaskId)}, {nameof(TaskBoardTaskEntity.LaneId)}, {nameof(TaskBoardTaskEntity.CategoryId)}, {nameof(TaskBoardTaskEntity.RunningNumber)}, {nameof(TaskBoardTaskEntity.RunningNumberId)}, {nameof(TaskBoardTaskEntity.Title)} " +
                      $"FROM {TaskEntity.TableNameConstant} " +
                      $"JOIN {LaneEntity.TableNameConstant} ON {LaneEntity.TableNameConstant}.{nameof(LaneEntity.LaneId)} = {TaskEntity.TableNameConstant}.{nameof(TaskEntity.LaneId)} " +
                      $"WHERE {nameof(LaneEntity.IsCompletedLane)} = 0 AND {nameof(LaneEntity.IsRejectedLane)} = 0;";

            using (var multiSelect = await connection.QueryMultipleAsync(sql))
            {
                var lanes = await multiSelect.ReadAsync<TaskBoardLaneEntity>();
                var tasks = await multiSelect.ReadAsync<TaskBoardTaskEntity>();

                foreach (var taskBoardLaneEntity in lanes)
                {
                    taskBoardLaneEntity.Tasks = new List<TaskBoardTaskEntity>(tasks.Where(x => x.LaneId == taskBoardLaneEntity.LaneId));
                }
            }

            return tasksOnBoard;
        }
    }
}