using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class AchievementCompletionRepository : Repository<AchievementCompletion>, IAchievementCompletionRepository
    {
        public AchievementCompletionRepository(DatabaseFileHandler fileHandler) : base(fileHandler)
        {
        }

        #region IAchievementCompletionRepository Members

        public async Task<IList<AchievementCompletion>> GetAchievementCompletionsAsync(Guid achievementId)
        {
            using (var connection = this.FileHandler.OpenConnection())
            {
                return await this.GetAchievementCompletionsAsync(connection, achievementId);
            }
        }

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