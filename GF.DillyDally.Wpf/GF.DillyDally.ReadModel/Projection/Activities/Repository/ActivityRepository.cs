using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Projection.Images.Repository;

namespace GF.DillyDally.ReadModel.Projection.Activities.Repository
{
    public sealed class ActivityRepository : Repository<ActivityEntity>
    {
        internal async Task CreateNewAsync(IDbConnection connection, Guid activityId, string activityName,
            ActivityType activityType)
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

        internal async Task AssignPreviewImageAsync(IDbConnection connection, Guid activityId, Guid? originalFileId)
        {
            var sql = $@"UPDATE {ActivityEntity.TableNameConstant} 
SET PreviewImageFileId = @originalFileId 
WHERE ActivityId = @activityId;";

            await connection.ExecuteAsync(sql, new {activityId, originalFileId});
        }

        public async Task<IEnumerable<ActivitySearchResultEntity>> SearchActivitiesByTextAsync(IDbConnection connection,
            string searchText)
        {
            var searchParameter = $"%{searchText ?? string.Empty}%";

            var sql =
                $"SELECT {nameof(ActivitySearchResultEntity.ActivityId)}, " +
                $"{nameof(ActivitySearchResultEntity.Name)}, " +
                $"{nameof(ActivitySearchResultEntity.ActivityType)}, " +
                $"{nameof(ActivitySearchResultEntity.ActivityValue)}, " +
                $"{nameof(ActivitySearchResultEntity.CurrentLevel)}, " +
                $"{nameof(ImageEntity.Binary)} AS {nameof(ActivitySearchResultEntity.PreviewImageBinary)}, " +
                $"1 AS {nameof(ActivitySearchResultEntity.Usages)} " +
                $"FROM {ActivityEntity.TableNameConstant} " +
                $"LEFT JOIN {ImageEntity.TableNameConstant} ON {ActivityEntity.TableNameConstant}.{nameof(ActivityEntity.PreviewImageFileId)} = {ImageEntity.TableNameConstant}.{nameof(ImageEntity.ImageId)} " +
                $"WHERE {nameof(ActivitySearchResultEntity.Name)} LIKE @{nameof(searchParameter)};";


            return await connection.QueryAsync<ActivitySearchResultEntity>(sql, new {searchParameter});
        }
    }
}