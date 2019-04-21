using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Entities
{
    [Dapper.Contrib.Extensions.Table("OpenTasksView")]
    public sealed class OpenTaskEntity : IOpenTaskEntity
    {
        private TaskKey _taskKey;

        [Column("TaskId")]
        [ExplicitKey]
        public Guid TaskId { get; set; }

        #region IOpenTaskEntity Members

        public TaskKey TaskKey
        {
            get { return this._taskKey ?? (this._taskKey = new TaskKey(this.TaskId)); }
        }

        [Column("Name")]
        public string Name { get; set; }

        [Column("TaskType")]
        public TaskType TaskType { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("DueDate")]
        public DateTime? DueDate { get; set; }

        [Column("RewardCount")]
        public int RewardCount { get; set; }

        #endregion
    }
}