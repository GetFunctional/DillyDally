using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Repositories.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("TaskCompletion")]
    internal sealed class TaskCompletionEntity : ITaskCompletionEntity
    {
        private TaskCompletionKey _taskCompletionKey;
        private TaskKey _taskKey;

        [ExplicitKey]
        [Column("TaskCompletionId")]
        public Guid TaskCompletionId { get; set; }

        [Column("TaskId")]
        public Guid TaskId { get; set; }

        #region ITaskCompletionEntity Members

        public TaskCompletionKey TaskCompletionKey
        {
            get
            {
                return this._taskCompletionKey ??
                       (this._taskCompletionKey = new TaskCompletionKey(this.TaskCompletionId));
            }
        }

        public TaskKey TaskKey
        {
            get { return this._taskKey ?? (this._taskKey = new TaskKey(this.TaskId)); }
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