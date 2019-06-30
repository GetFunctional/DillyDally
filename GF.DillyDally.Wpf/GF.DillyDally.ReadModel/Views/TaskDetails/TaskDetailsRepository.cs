using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.ReadModel.Projection.Images.Repository;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.Shared.Images;

namespace GF.DillyDally.ReadModel.Views.TaskDetails
{
    public sealed class TaskDetailsRepository
    {
        public async Task<TaskDetailsEntity> GetTaskDetailsAsync(IDbConnection connection, Guid taskId)
        {
            var sql =
                $@"SELECT TaskId, CategoryId, rn.RunningNumber, Name, DueDate, CreatedOn, Description, DefinitionOfDone 
FROM {TaskEntity.TableNameConstant} te 
JOIN {RunningNumberEntity.TableNameConstant} rn ON rn.RunningNumberId = te.RunningNumberId 
WHERE TaskId = @taskId; 
SELECT ae.ActivityId, ae.Name, ae.Description, ae.ActivityType, ae.ActivityValue, ae.CurrentLevel, img.Binary AS PreviewImageBytes 
FROM {ActivityEntity.TableNameConstant} ae 
JOIN {TaskActivityEntity.TableNameConstant} tae ON tae.ActivityId = ae.ActivityId
LEFT JOIN {ImageEntity.TableNameConstant} img ON ae.PreviewImageFileId = img.OriginalFileId AND img.SizeType = {(int)ImageSizeType.PreviewSize}
WHERE tae.TaskId = @taskId;";

            using (var multiSelect = await connection.QueryMultipleAsync(sql, new {taskId}))
            {
                var taskEntity = await multiSelect.ReadSingleAsync<TaskDetailsEntity>();
                var taskActivities = await multiSelect.ReadAsync<TaskDetailsActivityEntity>();

                taskEntity.AssignTaskActivities(taskActivities);

                return taskEntity;
            }
        }
    }
}