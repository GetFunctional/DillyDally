using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;
using GF.DillyDally.Wpf.Theme.Controls.Layout;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    public class CreateTaskViewModel : ViewModelBase
    {
        private ActivityContainerViewModel _activityContainerViewModel;
        private IReactiveCommand _cancelProcessCommand;
        private IReactiveCommand _createTaskCommand;
        private ObservableCollection<IDisplayPage> _createTaskSteps;
        private IDisplayPage _currentPage;
        private TaskAchievementsViewModel _taskAchievementsViewModel;

        public CreateTaskViewModel()
        {
            this.CreateTaskSteps = new ObservableCollection<IDisplayPage>();
        }

        public IReactiveCommand CreateTaskCommand
        {
            get { return this._createTaskCommand; }
            set { this.RaiseAndSetIfChanged(ref this._createTaskCommand, value); }
        }

        public IReactiveCommand CancelProcessCommand
        {
            get { return this._cancelProcessCommand; }
            set { this.RaiseAndSetIfChanged(ref this._cancelProcessCommand, value); }
        }

        public TaskAchievementsViewModel TaskAchievementsViewModel
        {
            get { return this._taskAchievementsViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._taskAchievementsViewModel, value); }
        }


        public ActivityContainerViewModel ActivityContainerViewModel
        {
            get { return this._activityContainerViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._activityContainerViewModel, value); }
        }

        public ObservableCollection<IDisplayPage> CreateTaskSteps
        {
            get { return this._createTaskSteps; }
            set { this.RaiseAndSetIfChanged(ref this._createTaskSteps, value); }
        }

        public IDisplayPage CurrentPage
        {
            get { return this._currentPage; }
            set { this.RaiseAndSetIfChanged(ref this._currentPage, value); }
        }
    }
}