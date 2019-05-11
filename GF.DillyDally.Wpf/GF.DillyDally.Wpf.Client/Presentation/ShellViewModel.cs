using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public sealed class ShellViewModel : ViewModelBase
    {
        private ContentBrowserViewModel _contentBrowserViewModel;
        private HeaderMenuViewModel _headerMenuViewModel;
        private OverlayViewModel _overlayViewModel;

        public ContentBrowserViewModel ContentBrowserViewModel
        {
            get { return this._contentBrowserViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._contentBrowserViewModel, value); }
        }

        public HeaderMenuViewModel HeaderMenuViewModel
        {
            get { return this._headerMenuViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._headerMenuViewModel, value); }
        }

        public OverlayViewModel OverlayViewModel
        {
            get { return this._overlayViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._overlayViewModel, value); }
        }
    }
}