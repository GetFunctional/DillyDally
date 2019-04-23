using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.ReadModel.Deprecated.Tasks
{
    public interface ITasksRepository
    {
        Task<IList<IOpenTaskEntity>> GetOpenTasksAsync();
        Task<ITaskEntity> GetSpecificTaskAsync(TaskKey taskKey);
    }
}