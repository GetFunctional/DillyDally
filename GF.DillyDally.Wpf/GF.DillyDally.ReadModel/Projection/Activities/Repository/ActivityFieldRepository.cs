using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.ReadModel.Projection.Activities.Repository
{
    public sealed class ActivityFieldRepository : Repository<ActivityFieldEntity>
    {
        public async Task AttachFieldToActivityAsync(IDbConnection connection, Guid activityId,
            WriteModel.Domain.Activities.ActivityFieldType activityFieldType, string fieldName, string unitOfMeasure)
        {
            await connection.InsertAsync(new ActivityFieldEntity
                                         {
                                             ActivityId = activityId,
                                             Name = fieldName,
                                             FieldType = (ActivityFieldType)activityFieldType,
                                             UnitOfMeasure = unitOfMeasure,
                                             ActivityFieldId = this.GuidGenerator.GenerateGuid()
                                         });

        }
    }
}