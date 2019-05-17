using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Selectors.Category;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks.Create
{
    public class CreateTaskViewModel : ViewModelBase
    {
        private IReactiveCommand _cancelProcessCommand;
        private IReactiveCommand _createTaskCommand;
        private TaskAchievementsViewModel _taskAchievementsViewModel;
        private string _taskName;
        private CategoryViewModel _selectedCategory;

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

        public CategoryViewModel SelectedCategory
        {
            get
            {
                return this._selectedCategory;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._selectedCategory, value);
            }
        }
    }
}