using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Contracts.Keys
{
    [DataContract(Name = "TaskTemplateKey")]
    public sealed class TaskTemplateKey : IdentityKeyBase<TaskTemplateKey>
    {
        #region Constructors

        #region - Konstruktoren -

        public TaskTemplateKey(Guid taskTemplateId)
        {
            this.TaskTemplateId = taskTemplateId;
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        [DataMember(Name = "TaskTemplateId")]
        public Guid TaskTemplateId { get; }

        #endregion

        #endregion

        #region - Methoden oeffentlich -

        public override bool Equals(TaskTemplateKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.TaskTemplateId == other.TaskTemplateId;
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

            return this.Equals((TaskTemplateKey) obj);
        }

        public override int GetHashCode()
        {
            return this.TaskTemplateId.GetHashCode();
        }

        #endregion
    }
}