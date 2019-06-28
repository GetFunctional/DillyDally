using System;

namespace GF.DillyDally.ReadModel.Views.TaskDetails
{
    public class TaskDetailsImageEntity
    {
        public Guid ImageId { get; set; }

        public byte[] ImageBytesSmall { get; set; }

        public byte[] ImageBytesMedium { get; set; }

        public bool IsPreviewImage { get; set; }
    }
}