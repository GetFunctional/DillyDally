using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    public class CreateTaskViewModel : PagedContentViewModel
    {
        private IReactiveCommand _cancelProcessCommand;
        private IReactiveCommand _createTaskCommand;
        private TaskAchievementsViewModel _taskAchievementsViewModel;

        public IReactiveCommand CreateTaskCommand
        {
            get
            {
                return this._createTaskCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._createTaskCommand, value);
            }
        }

        public IReactiveCommand CancelProcessCommand
        {
            get
            {
                return this._cancelProcessCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._cancelProcessCommand, value);
            }
        }

        public TaskAchievementsViewModel TaskAchievementsViewModel
        {
            get
            {
                return this._taskAchievementsViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._taskAchievementsViewModel, value);
            }
        }
    }
}