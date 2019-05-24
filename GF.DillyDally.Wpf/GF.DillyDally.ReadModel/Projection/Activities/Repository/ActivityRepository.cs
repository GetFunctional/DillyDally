using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite.Repository.Base;

namespace GF.DillyDally.ReadModel.Projection.Activities.Repository
{
    internal sealed class ActivityRepository : Repository<ActivityEntity>
    {
        public async Task CreateNewAsync(IDbConnection connection, Guid activityId, string activityName, ActivityType activityType)
        {
            await connection.InsertAsync(new ActivityEntity
                                         {
                                             ActivityId = activityId,
                                             Name = activityName,
                                             ActivityValue = 0,
                                             CurrentLevel = 1,
                                             ActivityType = activityType
                                         });
        }

        public async Task AssignPreviewImageAsync(IDbConnection connection, Guid activityId, Guid? previewImageId)
        {
            var sql = $"UPDATE {ActivityEntity.TableNameConstant} " +
                      $"SET {nameof(ActivityEntity.PreviewImageId)} = @{nameof(previewImageId)} " +
                      $"WHERE {nameof(ActivityEntity.ActivityId)} = @{nameof(activityId)};";

            await connection.ExecuteAsync(sql, new {activityId, previewImageId});
        }
    }
}