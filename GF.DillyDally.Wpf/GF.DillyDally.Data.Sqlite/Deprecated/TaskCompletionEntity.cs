using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Deprecated
{
    [System.ComponentModel.DataAnnotations.Schema.Table("TaskCompletion")]
    public sealed class TaskCompletionEntity : ITaskCompletionEntity
    {
        private TaskCompletionKey _taskCompletionKey;
        private TaskKey _taskKey;

        internal TaskCompletionEntity()
        {
        }

        [ExplicitKey]
        [Column("TaskCompletionId")]
        public Guid TaskCompletionId { get; set; }

        [Column("TaskId")]
        public Guid TaskId { get; set; }

        #region ITaskCompletionEntity Members

        [Computed]
        public TaskCompletionKey TaskCompletionKey
        {
            get
            {
                return this._taskCompletionKey ??
                       (this._taskCompletionKey = new TaskCompletionKey(this.TaskCompletionId));
            }
        }

        [Computed]
        public TaskKey TaskKey
        {
            get
            {
                return this._taskKey ?? (this._taskKey = new TaskKey(this.TaskId));
            }
            set
            {
                this._taskKey = value;
                this.TaskId = value.TaskId;
            }
        }

        [Column("CompletedOn")]
        public DateTime CompletedOn { get; set; }

        #endregion
    }
}