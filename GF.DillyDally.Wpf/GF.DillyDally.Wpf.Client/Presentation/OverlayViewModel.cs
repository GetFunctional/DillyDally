using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public class OverlayViewModel : ViewModelBase
    {
        private IViewModel _overlayContent;
        private string _overlaySubtitle;
        private string _overlayTitle;

        public string OverlayTitle
        {
            get { return this._overlayTitle; }
            set { this.RaiseAndSetIfChanged(ref this._overlayTitle, value); }
        }

        public string OverlaySubtitle
        {
            get { return this._overlaySubtitle; }
            set { this.RaiseAndSetIfChanged(ref this._overlaySubtitle, value); }
        }

        public IViewModel OverlayContent
        {
            get { return this._overlayContent; }
            set
            {
                this.RaiseAndSetIfChanged(ref this._overlayContent, value);
            }
        }
    }
}