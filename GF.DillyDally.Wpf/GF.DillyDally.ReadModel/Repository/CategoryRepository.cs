using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(DatabaseFileHandler fileHandler) : base(fileHandler)
        {
        }
    }
}