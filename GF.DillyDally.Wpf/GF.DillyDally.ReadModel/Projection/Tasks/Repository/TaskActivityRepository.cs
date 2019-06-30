using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.ReadModel.Projection.Tasks.Repository
{
    internal class TaskActivityRepository : Repository<TaskActivityEntity>
    {
        public async Task LinkTaskToActivitiesAsync(IDbConnection connection, Guid taskId, ISet<Guid> activityIds)
        {
            var newLinks = activityIds.Select(actId => new TaskActivityEntity()
                                                       {
                                                           TaskId = taskId,
                                                           ActivityId = actId,
                                                           TaskActivityId = this.GuidGenerator.GenerateGuid()
                                                       }).ToList();

            await this.InsertMultipleAsync(connection, newLinks);
        }
    }
}