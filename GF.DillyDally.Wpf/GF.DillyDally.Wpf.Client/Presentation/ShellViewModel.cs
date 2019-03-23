using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public sealed class ShellViewModel : ViewModelBase
    {
        #region Fields, Constants

        #region - Felder privat -

        private ContentBrowserViewModel _contentBrowserViewModel;

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public ContentBrowserViewModel ContentBrowserViewModel
        {
            get { return this._contentBrowserViewModel; }
            set { this.SetField(ref this._contentBrowserViewModel, value); }
        }

        #endregion

        #endregion
    }
}