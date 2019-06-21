using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    public sealed class TaskBoardRepository
    {
        public async Task<IList<TaskBoardLaneEntity>> GetTaskBoardLanesAsync(IDbConnection connection)
        {
            var sql = $"SELECT {nameof(LaneEntity.LaneId)}, {nameof(LaneEntity.Name)} " +
                      $"FROM {LaneEntity.TableNameConstant} " +
                      $"WHERE {nameof(LaneEntity.IsCompletedLane)} = 0 AND {nameof(LaneEntity.IsRejectedLane)} = 0;" +
                      $"SELECT {TaskEntity.TableNameConstant}.{nameof(TaskEntity.TaskId)}, {LaneTaskEntity.TableNameConstant}.{nameof(LaneTaskEntity.LaneId)}, {nameof(TaskEntity.CategoryId)}, {nameof(RunningNumberEntity.RunningNumber)}, {TaskEntity.TableNameConstant}.{nameof(TaskEntity.RunningNumberId)}, {TaskEntity.TableNameConstant}.{nameof(TaskEntity.Name)} " +
                      $"FROM {TaskEntity.TableNameConstant} " +
                      $"JOIN {RunningNumberEntity.TableNameConstant} ON {RunningNumberEntity.TableNameConstant}.{nameof(RunningNumberEntity.RunningNumberId)} = {TaskEntity.TableNameConstant}.{nameof(TaskEntity.RunningNumberId)} " +
                      $"JOIN {LaneTaskEntity.TableNameConstant} ON {LaneTaskEntity.TableNameConstant}.{nameof(LaneTaskEntity.TaskId)} = {TaskEntity.TableNameConstant}.{nameof(TaskEntity.TaskId)} " +
                      $"JOIN {LaneEntity.TableNameConstant} ON {LaneEntity.TableNameConstant}.{nameof(LaneEntity.LaneId)} = {LaneTaskEntity.TableNameConstant}.{nameof(LaneTaskEntity.LaneId)} " +
                      $"WHERE {nameof(LaneEntity.IsCompletedLane)} = 0 AND {nameof(LaneEntity.IsRejectedLane)} = 0;" +
                      $"SELECT {nameof(TaskBoardCategoryEntity.CategoryId)}, {nameof(TaskBoardCategoryEntity.Name)}, {nameof(TaskBoardCategoryEntity.ColorCode)} " +
                      $"FROM {CategoryEntity.TableNameConstant};";

            using (var multiSelect = await connection.QueryMultipleAsync(sql))
            {
                var lanes= (await multiSelect.ReadAsync<TaskBoardLaneEntity>()).ToList();
                var tasks = await multiSelect.ReadAsync<TaskBoardTaskEntity>();
                var categories = await multiSelect.ReadAsync<TaskBoardCategoryEntity>();

                foreach (var categoryEntity in categories)
                {
                    foreach (var tsk in tasks.Where(x => x.CategoryId == categoryEntity.CategoryId))
                    {
                        tsk.Category = categoryEntity;
                    }
                }

                foreach (var taskBoardLaneEntity in lanes)
                {
                    taskBoardLaneEntity.Tasks = new List<TaskBoardTaskEntity>(tasks.Where(x => x.LaneId == taskBoardLaneEntity.LaneId));
                }

                return lanes;
            }
        }
    }
}