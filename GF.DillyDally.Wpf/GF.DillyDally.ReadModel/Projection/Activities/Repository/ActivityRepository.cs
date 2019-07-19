using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Images.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.Shared.Images;

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
            var searchParameter = $"%{(searchText ?? string.Empty).Trim()}%";

            var sql =
                $@"SELECT a.ActivityId, a.Name, a.ActivityType, a.ActivityValue, a.CurrentLevel, img.Binary AS PreviewImageBinary,
( SELECT COUNT(TaskId) FROM {TaskActivityEntity.TableNameConstant} ta WHERE ta.ActivityId = a.ActivityId GROUP BY ta.ActivityId ) AS Usages 
FROM {ActivityEntity.TableNameConstant} AS a 
LEFT JOIN {ImageEntity.TableNameConstant} img ON a.PreviewImageFileId = img.OriginalFileId AND img.SizeType = {(int)ImageSizeType.PreviewSize} 
WHERE a.Name LIKE @searchParameter 
{this.LimitResults(searchText)};";

            return await connection.QueryAsync<ActivitySearchResultEntity>(sql, new {searchParameter});
        }

        private string LimitResults(string searchParameter)
        {
            return string.IsNullOrWhiteSpace(searchParameter) || string.IsNullOrEmpty(searchParameter)
                ? "LIMIT 10"
                : string.Empty;
        }
    }
}