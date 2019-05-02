using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    internal class TaskLink : IEquatable<TaskLink>
    {
        public TaskLink(Guid leftTaskId, Guid rightTaskId)
        {
            this.LeftTaskId = leftTaskId;
            this.RightTaskId = rightTaskId;
        }

        public Guid LeftTaskId { get; }

        public Guid RightTaskId { get; }

        #region IEquatable<TaskLink> Members

        public bool Equals(TaskLink other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.LeftTaskId.Equals(other.LeftTaskId) && this.RightTaskId.Equals(other.RightTaskId) ||
                   this.LeftTaskId.Equals(other.RightTaskId) && this.RightTaskId.Equals(other.LeftTaskId);
        }

        #endregion

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

            return this.Equals((TaskLink)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.LeftTaskId.GetHashCode() * 397) ^ this.RightTaskId.GetHashCode();
            }
        }
    }
}