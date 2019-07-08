using System.Collections.Generic;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container
{
    internal sealed class ImageContainerController : DDControllerBase<ImageContainerViewModel>
    {
        public ImageContainerController(ImageContainerViewModel viewModel, IControllerServices controllerServices) : base(viewModel, controllerServices)
        {
        }

        public void AssignImages(IEnumerable<ImageContainerItemViewModel> images)
        {
            this.ViewModel.AddImages(images);
        }
    }
}