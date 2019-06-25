using System;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    public class TaskBoardTaskViewModel : ViewModelBase
    {
        private string _category;
        private string _colorString;
        private string _name;
        private string _runningNumber;
        private int _storypoints;
        private IReactiveCommand _openTaskDetailsCommand;

        public TaskBoardTaskViewModel(Guid taskId, string name, string runningNumber, string colorString, string category, int storypoints)
        {
            this.TaskId = taskId;
            this.Name = name;
            this.RunningNumber = runningNumber;
            this.ColorString = colorString;
            this.Category = category;
            this.Storypoints = storypoints;
        }

        public Guid TaskId { get; }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._name, value);
            }
        }

        public string RunningNumber
        {
            get
            {
                return this._runningNumber;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._runningNumber, value);
            }
        }

        public string ColorString
        {
            get
            {
                return this._colorString;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._colorString, value);
            }
        }

        public string Category
        {
            get
            {
                return this._category;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._category, value);
            }
        }

        public int Storypoints
        {
            get
            {
                return this._storypoints;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._storypoints, value);
            }
        }

        public IReactiveCommand OpenTaskDetailsCommand
        {
            get { return this._openTaskDetailsCommand; }
            set { this.RaiseAndSetIfChanged(ref this._openTaskDetailsCommand, value); }
        }
    }
}