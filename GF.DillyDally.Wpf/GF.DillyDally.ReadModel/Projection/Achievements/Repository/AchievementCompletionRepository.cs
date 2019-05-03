using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;

namespace GF.DillyDally.ReadModel.Projection.Achievements.Repository
{
    internal class AchievementCompletionRepository : Repository<AchievementCompletion>
    {
        #region IAchievementCompletionRepository Members

        public async Task<IList<AchievementCompletion>> GetAchievementCompletionsAsync(IDbConnection connection,
            Guid achievementId)
        {
            var querySql =
                $"SELECT * FROM {AchievementCompletion.TableNameConstant} WHERE {nameof(AchievementCompletion.AchievementId)} = @id";
            return (await connection.QueryAsync<AchievementCompletion>(querySql, new {id = achievementId})).ToList();
        }

        #endregion
    }
}