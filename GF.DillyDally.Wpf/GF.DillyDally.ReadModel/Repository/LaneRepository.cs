using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class LaneRepository : Repository<LaneEntity> , ILaneRepository
    {
        public LaneRepository(DatabaseFileHandler fileHandler) : base(fileHandler)
        {
        }
    }
}