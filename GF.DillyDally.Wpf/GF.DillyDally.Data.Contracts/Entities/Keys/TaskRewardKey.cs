using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "TaskRewardKey")]
    public sealed class TaskRewardKey : IdentityKeyBase<TaskRewardKey>
    {
        public TaskRewardKey(Guid taskRewardId)
        {
            this.TaskRewardId = taskRewardId;
        }

        [DataMember(Name = "TaskRewardId")]
        public Guid TaskRewardId { get; }

        public override bool Equals(TaskRewardKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.TaskRewardId == other.TaskRewardId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((TaskRewardKey)obj);
        }

        public override int GetHashCode()
        {
            return this.TaskRewardId.GetHashCode();
        }
    }
}