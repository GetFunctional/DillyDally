using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Table(TableNameConstant)]
    public class CategoryEntity
    {
        public const string TableNameConstant = "Categories";

        [ExplicitKey]
        public Guid CategoryId { get; set; }

        public Guid RunningNumberId { get; set; }

        public string Name { get; set; }

        public string ColorCode { get; set; }
    }
}