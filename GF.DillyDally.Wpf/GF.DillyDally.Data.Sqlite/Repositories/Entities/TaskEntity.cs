using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Repositories.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Task")]
    internal sealed class TaskEntity : ITaskEntity
    {
        private TaskKey _taskKey;

        [ExplicitKey]
        [Column("TaskId")]
        public Guid TaskId { get; set; }

        #region ITaskEntity Members

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

        #endregion
    }
}