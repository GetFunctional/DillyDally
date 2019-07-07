using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.ReadModel.Projection.Achievements.Repository
{
    internal sealed class AchievementRepository : Repository<AchievementEntity>
    {
        public async Task CompletedAsync(IDbConnection connection, Guid achievementId, DateTime? completedOn)
        {
            var sql = $"UPDATE {AchievementEntity.TableNameConstant} " +
                      $"SET {nameof(AchievementEntity.CompletedOn)} = @{nameof(completedOn)} " +
                      $"WHERE {nameof(AchievementEntity.AchievementId)} = @{nameof(achievementId)};";

            await connection.ExecuteAsync(sql, new {achievementId, completedOn});
        }

        public async Task CreateNewAsync(IDbConnection connection, Guid achievementId, string achievementName,
            Guid runningNumberId)
        {
            await connection.InsertAsync(new AchievementEntity
            {
                AchievementId = achievementId,
                Name = achievementName,
                RunningNumberId = runningNumberId
            });
        }
    }
}