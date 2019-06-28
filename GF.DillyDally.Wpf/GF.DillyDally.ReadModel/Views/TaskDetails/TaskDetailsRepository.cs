using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.ReadModel.Views.TaskDetails;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    public sealed class TaskDetailsRepository
    {
        public async Task<TaskDetailsEntity> GetTaskDetailsAsync(IDbConnection connection, Guid taskId)
        {
            var sql = $"SELECT TaskId, CategoryId, {RunningNumberEntity.TableNameConstant}.RunningNumber, Name, DueDate, CreatedOn, Description, DefinitionOfDone " +
                      $"FROM {TaskEntity.TableNameConstant} " +
                      $"JOIN {RunningNumberEntity.TableNameConstant} ON {RunningNumberEntity.TableNameConstant}.RunningNumberId = {TaskEntity.TableNameConstant}.RunningNumberId " +
                      $"WHERE TaskId = @taskId;";

            using (var multiSelect = await connection.QueryMultipleAsync(sql, new {taskId}))
            {
                var taskEntity = (await multiSelect.ReadAsync<TaskDetailsEntity>()).Single();
                //var tasks = await multiSelect.ReadAsync<TaskBoardTaskEntity>();
                //var categories = await multiSelect.ReadAsync<TaskBoardCategoryEntity>();

                //foreach (var categoryEntity in categories)
                //{
                //    foreach (var tsk in tasks.Where(x => x.CategoryId == categoryEntity.CategoryId))
                //    {
                //        tsk.Category = categoryEntity;
                //    }
                //}

                //foreach (var taskBoardLaneEntity in lanes)
                //{
                //    taskBoardLaneEntity.Tasks = new List<TaskBoardTaskEntity>(tasks.Where(x => x.LaneId == taskBoardLaneEntity.LaneId));
                //}

                return taskEntity;
            }
        }
    }
}