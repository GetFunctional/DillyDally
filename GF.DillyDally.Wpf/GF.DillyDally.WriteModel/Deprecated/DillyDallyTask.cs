using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.WriteModel.Deprecated
{
    internal sealed class DillyDallyTask : ITask
    {
        private readonly List<ITaskReward> _rewards;

        public DillyDallyTask(TaskKey taskKey, string name, TaskType taskType)
        {
            this.TaskKey = taskKey;
            this.Name = name;
            this.TaskType = taskType;
            this._rewards = new List<ITaskReward>();
        }

        #region ITask Members

        public TaskKey TaskKey { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskType TaskType { get; private set; }

        public IReadOnlyList<ITaskReward> Rewards
        {
            get { return this._rewards; }
        }

        public void AddReward(ITaskReward taskReward)
        {
            this._rewards.Add(taskReward);
        }

        public void RemoveReward(TaskRewardKey taskRewardKey)
        {
            var rewardFromList = this.Rewards.Single(x => x.TaskRewardKey == taskRewardKey);
            this._rewards.Remove(rewardFromList);
        }

        public void ChangeTaskType(TaskType taskType)
        {
            this.TaskType = taskType;
        }

        #endregion
    }
}