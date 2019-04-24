using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.ReadModel.Repository.Base;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Dapper.Contrib.Extensions.Table(TableNameConstant)]
    public class Category
    {
        public const string TableNameConstant = "Categories";

        [ExplicitKey]
        public Guid CategoryId { get; set; }

        public Guid RunningNumberId { get; set; }

        public string Name { get; set; }

        public string ColorCode { get; set; }
    }
}