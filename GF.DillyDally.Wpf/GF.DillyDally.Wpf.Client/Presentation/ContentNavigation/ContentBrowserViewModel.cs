using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentBrowserViewModel : ViewModelBase
    {
        #region Fields, Constants

        #region - Felder privat -

        private ContentNavigatorViewModel _currentActiveNavigator;

        #endregion

        #endregion

        #region Constructors

        #region - Konstruktoren -

        public ContentBrowserViewModel()
        {
            this.ActiveNavigators = new ObservableCollection<ContentNavigatorViewModel>();
        }

        #endregion

        #endregion

        #region - Methoden oeffentlich -

        public void SelectCurrentNavigator(ContentNavigatorViewModel contentNavigatorViewModel)
        {
            if (!this.ActiveNavigators.Contains(contentNavigatorViewModel))
            {
                this.ActiveNavigators.Add(contentNavigatorViewModel);
            }

            this.CurrentActiveNavigator = contentNavigatorViewModel;
        }

        #endregion

        #region - Properties oeffentlich -

        public ObservableCollection<ContentNavigatorViewModel> ActiveNavigators { get; set; }

        public ContentNavigatorViewModel CurrentActiveNavigator
        {
            get { return this._currentActiveNavigator; }
            set { this.SetField(ref this._currentActiveNavigator, value); }
        }

        public CloseNavigatorCommand CloseNavigatorCommand { get; }

        public AddNewNavigatorCommand AddNewNavigatorCommand { get; }

        #endregion
    }
}