using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public sealed class TasksListViewModel : ViewModelBase
    {
        private AddTaskCommand _addTaskCommand;
        private ObservableCollection<TaskViewModel> _tasks;

        public ObservableCollection<TaskViewModel> Tasks
        {
            get { return this._tasks; }
            set { this.SetField(ref this._tasks, value); }
        }

        public AddTaskCommand AddTaskCommand
        {
            get { return this._addTaskCommand; }
            set { this.SetField(ref this._addTaskCommand, value); }
        }
    }
}