using System;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskDetailsViewModel : ViewModelBase
    {
        private DateTime? _dueDate;
        private string _taskName;

        public DateTime? DueDate
        {
            get
            {
                return this._dueDate;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._dueDate, value);
            }
        }

        public string TaskName
        {
            get
            {
                return this._taskName;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._taskName, value);
            }
        }
    }
}