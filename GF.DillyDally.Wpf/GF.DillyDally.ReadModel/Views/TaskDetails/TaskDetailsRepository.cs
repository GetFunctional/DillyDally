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
                $@"
-- Select TaskDetails
SELECT TaskId, CategoryId, rn.RunningNumber, Name, DueDate, CreatedOn, Description, DefinitionOfDone 
FROM {TaskEntity.TableNameConstant} te 
JOIN {RunningNumberEntity.TableNameConstant} rn ON rn.RunningNumberId = te.RunningNumberId 
WHERE TaskId = @taskId; 

-- Select Activites
SELECT ae.ActivityId, ae.Name, ae.Description, ae.ActivityType, ae.ActivityValue, ae.CurrentLevel, img.Binary AS PreviewImageBytes 
FROM {ActivityEntity.TableNameConstant} ae 
JOIN {TaskActivityEntity.TableNameConstant} tae ON tae.ActivityId = ae.ActivityId
LEFT JOIN {ImageEntity.TableNameConstant} img ON ae.PreviewImageFileId = img.OriginalFileId AND img.SizeType = {(int) ImageSizeType.PreviewSize}
WHERE tae.TaskId = @taskId;

-- Select Activity Fields
SELECT aef.ActivityFieldId, aef.StringValue, aef.DateTimeValue, aef.IntegerValue, aef.DecimalValue, aef.IntegerValue, aef.BooleanValue, af.ActivityId, af.Name, af.FieldType, af.UnitOfMeasure
FROM {TaskActivityEntity.TableNameConstant} ta
LEFT JOIN {ActivityFieldEntity.TableNameConstant} af ON ta.ActivityId = af.ActivityId
LEFT JOIN {TaskActivityFieldValueEntity.TableNameConstant} aef ON aef.ActivityFieldId = af.ActivityFieldId
WHERE ta.TaskId = @taskId;

-- Select Images
SELECT ti.OriginalFileId, 
( SELECT Images.Binary FROM {ImageEntity.TableNameConstant} WHERE Images.OriginalFileId = img.OriginalFileId AND Images.SizeType = 0 ) AS ImageBytesSmall,
( SELECT Images.Binary FROM {ImageEntity.TableNameConstant} WHERE Images.OriginalFileId = img.OriginalFileId AND Images.SizeType = 1 ) AS ImageBytesMedium,
(CASE WHEN ti.OriginalFileId = t.PreviewImageFileId THEN TRUE ELSE FALSE END) AS IsPreviewImage
FROM {TaskImageEntity.TableNameConstant} ti
JOIN {ImageEntity.TableNameConstant} img ON img.OriginalFileId = ti.OriginalFileId 
JOIN Tasks t ON ti.TaskId = t.TaskId 
WHERE ti.TaskId = @taskId
GROUP BY ti.OriginalFileId;";

            using (var multiSelect = await connection.QueryMultipleAsync(sql, new {taskId}))
            {
                var taskEntity = await multiSelect.ReadSingleAsync<TaskDetailsEntity>();
                var taskActivities = await multiSelect.ReadAsync<TaskDetailsActivityEntity>();
                var taskActivityFields = await multiSelect.ReadAsync<TaskActivityFieldEntity>();
                var taskImages = await multiSelect.ReadAsync<TaskDetailsImageEntity>();

                taskEntity.AssignTaskActivities(taskActivities);
                taskEntity.AssignTaskActivityFields(taskActivityFields);
                taskEntity.AssignTaskImages(taskImages);
                return taskEntity;
            }
        }
    }
}