using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    internal class TaskBoardRepository
    {
        public async Task<IList<TaskBoardLaneEntity>> GetTaskBoardLanesAsync(IDbConnection connection)
        {
            return await Task.Run(() =>
            {
                return new List<TaskBoardLaneEntity>();
            });
        }
    }
}