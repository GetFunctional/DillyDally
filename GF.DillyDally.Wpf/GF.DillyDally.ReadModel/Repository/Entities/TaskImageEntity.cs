using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Table(TableNameConstant)]
    public class TaskImageEntity
    {
        public const string TableNameConstant = "TaskImages";

        [ExplicitKey]
        public Guid TaskImageId { get; set; }

        public Guid TaskId { get; set; }

        public Guid ImageId { get; set; }
    }
}