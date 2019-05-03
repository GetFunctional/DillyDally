using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    public interface ITaskLinksRepository : IRepository<TaskLinkEntity>
    {
        Task<IList<TaskLinkEntity>> GetLinksForTaskIdAsync(IDbConnection connection, Guid taskId);
    }
}