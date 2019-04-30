using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class TaskRepository : Repository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(DatabaseFileHandler fileHandler) : base(fileHandler)
        {
        }
    }
}