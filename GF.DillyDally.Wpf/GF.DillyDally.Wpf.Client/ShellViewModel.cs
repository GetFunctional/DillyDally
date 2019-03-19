using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.ContentNavigation;

namespace GF.DillyDally.Wpf.Client
{
    public sealed class ShellViewModel : ViewModelBase
    {
        #region - Felder privat -

        private ContentBrowserViewModel _contentBrowserViewModel;

        #endregion

        #region - Properties oeffentlich -

        public ContentBrowserViewModel ContentBrowserViewModel
        {
            get { return this._contentBrowserViewModel; }
            set { this.SetField(ref this._contentBrowserViewModel, value); }
        }

        #endregion
    }
}