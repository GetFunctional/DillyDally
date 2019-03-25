using System;
using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public sealed class TaskViewModel : ViewModelBase
    {
        public TaskKey TaskKey { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Workload Workload { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}