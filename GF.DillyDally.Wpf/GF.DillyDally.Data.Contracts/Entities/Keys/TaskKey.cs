using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "TaskKey")]
    public sealed class TaskKey : IdentityKeyBase<TaskKey>
    {
        public TaskKey(Guid taskId)
        {
            this.TaskId = taskId;
        }

        [DataMember(Name = "TaskId")]
        public Guid TaskId { get; }

        public override bool Equals(TaskKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.TaskId == other.TaskId;
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

            return this.Equals((TaskKey)obj);
        }

        public override int GetHashCode()
        {
            return this.TaskId.GetHashCode();
        }
    }
}