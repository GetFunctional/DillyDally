using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class AchievementRepository : Repository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(DatabaseFileHandler fileHandler) : base(fileHandler)
        {
        }
    }
}