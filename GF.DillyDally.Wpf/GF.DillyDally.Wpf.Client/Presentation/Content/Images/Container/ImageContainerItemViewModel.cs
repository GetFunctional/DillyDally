using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container
{
    public class ImageContainerItemViewModel : ViewModelBase
    {
        public ImageContainerItemViewModel(byte[] imagePreview)
        {
            this.ImagePreview = imagePreview;
        }


        public byte[] ImagePreview { get; }
    }
}