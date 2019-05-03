using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;

namespace GF.DillyDally.ReadModel.Projection.Achievements.Repository
{
    internal sealed class AchievementRepository : Repository<AchievementEntity>
    {
        public async Task ChangeCounterValueAsync(IDbConnection connection, Guid achievementId,
            int newCounterValue)
        {
            await connection.ExecuteAsync(
                $"UPDATE {AchievementEntity.TableNameConstant} " +
                $"SET {nameof(AchievementEntity.CounterIncrease)} = @counterIncrease " +
                $"WHERE {nameof(AchievementEntity.AchievementId)} = @achievementId;",
                new {counterIncrease = newCounterValue, achievementId});
        }
    }
}