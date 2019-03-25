using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    internal class TaskTemplate
    {
        public TaskTemplateKey TaskTemplateKey { get; }
        public string Name { get; }
        public string Description { get; }
        public Workload Workload { get; }
    }
}