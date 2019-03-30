using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "TaskCompletionKey")]
    public sealed class TaskCompletionKey : IdentityKeyBase<TaskCompletionKey>
    {
        public TaskCompletionKey(Guid taskCompletionId)
        {
            this.TaskCompletionId = taskCompletionId;
        }

        [DataMember(Name = "TaskCompletionId")]
        public Guid TaskCompletionId { get; }

        public override bool Equals(TaskCompletionKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.TaskCompletionId == other.TaskCompletionId;
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

            return this.Equals((TaskCompletionKey) obj);
        }

        public override int GetHashCode()
        {
            return this.TaskCompletionId.GetHashCode();
        }
    }
}