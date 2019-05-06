using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public sealed class ShellViewModel : ViewModelBase
    {
        private ContentBrowserViewModel _contentBrowserViewModel;

        public ContentBrowserViewModel ContentBrowserViewModel
        {
            get
            {
                return this._contentBrowserViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._contentBrowserViewModel, value);
            }
        }
    }
}