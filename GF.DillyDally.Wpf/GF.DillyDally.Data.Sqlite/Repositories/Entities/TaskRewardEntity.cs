using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Repositories.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("TaskReward")]
    internal sealed class TaskRewardEntity : ITaskRewardEntity
    {
        private RewardKey _rewardKey;
        private TaskKey _taskKey;
        private TaskRewardKey _taskRewardKey;

        [ExplicitKey]
        [Column("TaskRewardId")]
        public Guid TaskRewardId { get; set; }

        [Column("TaskId")]
        public Guid TaskId { get; set; }

        [Column("RewardId")]
        public Guid RewardId { get; set; }

        #region ITaskRewardEntity Members

        public TaskRewardKey TaskRewardKey
        {
            get { return this._taskRewardKey ?? (this._taskRewardKey = new TaskRewardKey(this.TaskRewardId)); }
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

        public RewardKey RewardKey
        {
            get { return this._rewardKey ?? (this._rewardKey = new RewardKey(this.RewardId)); }
            set
            {
                this._rewardKey = value;
                this.RewardId = value.RewardId;
            }
        }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("ClaimedOn")]
        public DateTime? ClaimedOn { get; set; }

        #endregion
    }
}