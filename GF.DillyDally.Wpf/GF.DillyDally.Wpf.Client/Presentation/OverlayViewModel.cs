using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    public class OverlayViewModel : ViewModelBase
    {
        private IViewModel _overlayContent;

        public IViewModel OverlayContent
        {
            get { return this._overlayContent; }
            set { this.SetAndRaiseIfChanged(ref this._overlayContent, value); }
        }
    }
}