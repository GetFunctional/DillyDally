using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public class OverlayViewModel : ViewModelBase
    {
        private IViewModel _overlayContent;

        public IViewModel OverlayContent
        {
            get { return this._overlayContent; }
            set { this.RaiseAndSetIfChanged(ref this._overlayContent, value); }
        }
    }
}