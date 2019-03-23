using GF.DillyDally.Contracts;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Tasks
{
    public sealed class TaskTemplate
    {
        #region Properties, Indexers

        public TaskTemplateKey TaskTemplateKey { get; }
        public string Name { get; }
        public string Description { get; }
        public Workload Workload { get; }

        #endregion
    }
}