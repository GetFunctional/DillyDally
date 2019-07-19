using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskSummaryViewModel : ViewModelBase
    {
        private string _taskName;
        private string _definitionOfDone;
        private string _description;
        private DateTime? _dueDate;
        private byte[] _taskPreviewImageBytes;
        private string _runningNumber;

        public string RunningNumber
        {
            get
            {
                return this._runningNumber;
            }
            set { this.SetAndRaiseIfChanged(ref this._runningNumber, value); }
        }

        public DateTime? DueDate
        {
            get { return this._dueDate; }
            set { this.SetAndRaiseIfChanged(ref this._dueDate, value); }
        }

        public string TaskName
        {
            get { return this._taskName; }
            set { this.SetAndRaiseIfChanged(ref this._taskName, value); }
        }

        public string DefinitionOfDone
        {
            get { return this._definitionOfDone; }
            set { this.SetAndRaiseIfChanged(ref this._definitionOfDone, value); }
        }

        public string Description
        {
            get { return this._description; }
            set { this.SetAndRaiseIfChanged(ref this._description, value); }
        }

        public byte[] TaskPreviewImageBytes
        {
            get { return this._taskPreviewImageBytes; }
            set { this.SetAndRaiseIfChanged(ref this._taskPreviewImageBytes, value); }
        }
    }
}