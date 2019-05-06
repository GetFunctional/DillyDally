using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentBrowserViewModel : ViewModelBase
    {
        private ContentNavigatorViewModel _currentActiveNavigator;

        public ContentBrowserViewModel()
        {
            this.ActiveNavigators = new ObservableCollection<ContentNavigatorViewModel>();
        }

        public ObservableCollection<ContentNavigatorViewModel> ActiveNavigators { get; set; }

        public ContentNavigatorViewModel CurrentActiveNavigator
        {
            get
            {
                return this._currentActiveNavigator;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._currentActiveNavigator, value);
            }
        }

        public CloseNavigatorCommand CloseNavigatorCommand { get; }

        public AddNewNavigatorCommand AddNewNavigatorCommand { get; }

        public void SelectCurrentNavigator(ContentNavigatorViewModel contentNavigatorViewModel)
        {
            if (!this.ActiveNavigators.Contains(contentNavigatorViewModel))
            {
                this.ActiveNavigators.Add(contentNavigatorViewModel);
            }

            this.CurrentActiveNavigator = contentNavigatorViewModel;
        }
    }
}