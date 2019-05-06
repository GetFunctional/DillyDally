using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public sealed class ShellViewModel : ViewModelBase
    {
        private ContentBrowserViewModel _contentBrowserViewModel;
        private IViewModel _overlayContent;

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
    }
}