using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container
{
    public class ImageContainerViewModel : ViewModelBase
    {
        private IList<ImageContainerItemViewModel> _images;

        public ImageContainerViewModel()
        {
            this._images = new List<ImageContainerItemViewModel>();
        }

        public IList<ImageContainerItemViewModel> Images
        {
            get
            {
                return this._images;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._images, value);
            }
        }

        internal void AddImages(IEnumerable<ImageContainerItemViewModel> newImages)
        {
            var images = this.Images.Concat(newImages).Distinct().ToList();
            this.Images = images;
        }
    }
}