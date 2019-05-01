using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class FileRepository : Repository<FileEntity>, IFileRepository
    {
        public FileRepository(DatabaseFileHandler fileHandler) : base(fileHandler)
        {
        }
    }
}