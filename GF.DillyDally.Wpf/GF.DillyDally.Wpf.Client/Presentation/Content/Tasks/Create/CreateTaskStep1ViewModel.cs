using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Category;
using GF.DillyDally.Wpf.Theme.Controls.Layout;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    internal sealed class CreateTaskStep1ViewModel : ViewModelBase, IDisplayPage
    {
        private CategorySelectorViewModel _categorySelectorViewModel;
        private bool _isCurrent;
        private int _pageNumber;
        private string _taskName;

        public CreateTaskStep1ViewModel(CategorySelectorViewModel categorySelectorViewModel)
        {
            this._categorySelectorViewModel = categorySelectorViewModel;
        }


        public string TaskName
        {
            get { return this._taskName; }
            set { this.RaiseAndSetIfChanged(ref this._taskName, value); }
        }

        public CategoryViewModel SelectedCategory
        {
            get { return this._categorySelectorViewModel.SelectedCategory; }
        }

        public CategorySelectorViewModel CategorySelectorViewModel
        {
            get { return this._categorySelectorViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._categorySelectorViewModel, value); }
        }

        #region IDisplayPage Members

        public string Title { get; } = "Task Infos";

        public bool IsCurrent
        {
            get { return this._isCurrent; }
            set { this.RaiseAndSetIfChanged(ref this._isCurrent, value); }
        }

        public int PageNumber
        {
            get { return this._pageNumber; }
            set { this.RaiseAndSetIfChanged(ref this._pageNumber, value); }
        }

        #endregion
    }
}