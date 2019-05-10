using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public sealed class ShellViewModel : ViewModelBase
    {
        private ContentBrowserViewModel _contentBrowserViewModel;
        private IViewModel _overlayContent;
        private HeaderMenuViewModel _headerMenuViewModel;
        private DialogSize _overlaySize;

        public ContentBrowserViewModel ContentBrowserViewModel
        {
            get { return this._contentBrowserViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._contentBrowserViewModel, value); }
        }

        public IViewModel OverlayContent
        {
            get { return this._overlayContent; }
            set { this.RaiseAndSetIfChanged(ref this._overlayContent, value); }
        }

        public HeaderMenuViewModel HeaderMenuViewModel
        {
            get { return this._headerMenuViewModel; }
            set { this.RaiseAndSetIfChanged(ref this._headerMenuViewModel, value); }
        }

        public DialogSize OverlaySize
        {
            get { return this._overlaySize; }
            set { this.RaiseAndSetIfChanged(ref this._overlaySize, value); }
        }
    }
}