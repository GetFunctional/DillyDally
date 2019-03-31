using System;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public sealed class TaskViewModel : ViewModelBase
    {
        public TaskViewModel(TaskKey taskKey)
        {
            this.TaskKey = taskKey;
        }

        public TaskKey TaskKey { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}