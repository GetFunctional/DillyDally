using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Deprecated
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Task")]
    public sealed class TaskEntity : ITaskEntity
    {
        private TaskKey _taskKey;

        internal TaskEntity()
        {
        }

        [ExplicitKey]
        [Column("TaskId")]
        public Guid TaskId { get; set; }

        #region ITaskEntity Members

        [Computed]
        public TaskKey TaskKey
        {
            get { return this._taskKey ?? (this._taskKey = new TaskKey(this.TaskId)); }
        }

        [StringLength(255)]
        [Column("Name")]
        public string Name { get; set; }

        [Column("TaskType")]
        public TaskType TaskType { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("DueDate")]
        public DateTime? DueDate { get; set; }

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        #endregion
    }
}