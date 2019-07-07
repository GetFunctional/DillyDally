using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    public class CreateTaskViewModel : PagedContentViewModel
    {
        private ICommand _cancelProcessCommand;
        private ICommand _createTaskCommand;
        private TaskAchievementsViewModel _taskAchievementsViewModel;

        public ICommand CreateTaskCommand
        {
            get { return this._createTaskCommand; }
            set { this.SetAndRaiseIfChanged(ref this._createTaskCommand, value); }
        }

        public ICommand CancelProcessCommand
        {
            get { return this._cancelProcessCommand; }
            set { this.SetAndRaiseIfChanged(ref this._cancelProcessCommand, value); }
        }

        public TaskAchievementsViewModel TaskAchievementsViewModel
        {
            get { return this._taskAchievementsViewModel; }
            set { this.SetAndRaiseIfChanged(ref this._taskAchievementsViewModel, value); }
        }
    }
}