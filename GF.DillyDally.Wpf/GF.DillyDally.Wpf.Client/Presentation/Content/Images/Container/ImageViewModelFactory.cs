using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.ReadModel.Views.TaskDetails;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container
{
    internal class ImageViewModelFactory
    {
        public IEnumerable<ImageContainerItemViewModel> CreateViewModelFrom(IReadOnlyList<TaskDetailsImageEntity> taskImages)
        {
            return taskImages.Select(entity => new ImageContainerItemViewModel(entity.ImageBytesSmall));
        }
    }
}