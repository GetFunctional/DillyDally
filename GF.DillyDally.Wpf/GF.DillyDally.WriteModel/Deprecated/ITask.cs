using System;
using System.Collections.Generic;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.WriteModel.Deprecated
{
    public interface ITask
    {
        TaskKey TaskKey { get; }

        string Name { get; set; }

        string Description { get; set; }

        DateTime? DueDate { get; set; }

        TaskType TaskType { get; }

        IReadOnlyList<ITaskReward> Rewards { get; }

        void AddReward(ITaskReward taskReward);

        void RemoveReward(TaskRewardKey taskRewardKey);
        void ChangeTaskType(TaskType taskType);
    }
}