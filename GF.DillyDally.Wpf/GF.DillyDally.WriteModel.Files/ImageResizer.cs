using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GF.DillyDally.WriteModel.Files
{
    public class ImageResizer
    {
        public static Size GetPreviewSizeFor(Size sourceSize, ImageSizeType imageSizeType)
        {
            switch (imageSizeType)
            {
                default:
                case ImageSizeType.PreviewSize:
                    var previewMaxSize = new Size(600, 315);
                    return ResizeKeepAspect(sourceSize, previewMaxSize.Width, previewMaxSize.Height, true);
                case ImageSizeType.Small:
                    var smallMaxSize = new Size(1200, 630);
                    return ResizeKeepAspect(sourceSize, smallMaxSize.Width, smallMaxSize.Height, true);
                case ImageSizeType.Full:
                    var fullMaxSize = new Size(3000, 3000);
                    return ResizeKeepAspect(sourceSize, fullMaxSize.Width, fullMaxSize.Height, true);
            }
        }

        private static Size ResizeKeepAspect(Size src, int maxWidth, int maxHeight, bool enlarge = false)
        {
            maxWidth = enlarge ? maxWidth : Math.Min(maxWidth, src.Width);
            maxHeight = enlarge ? maxHeight : Math.Min(maxHeight, src.Height);

            var rnd = Math.Min(maxWidth / (decimal) src.Width, maxHeight / (decimal) src.Height);
            return new Size((int) Math.Round(src.Width * rnd), (int) Math.Round(src.Height * rnd));
        }


        public static byte[] CreateImagePreview(byte[] sourceBytes, ImageSizeType imageSizeType)
        {
            using (var myMemStream = new MemoryStream(sourceBytes))
            {
                using (var fullsizeImage = Image.FromStream(myMemStream))
                {
                    var destinationSize = GetPreviewSizeFor(fullsizeImage.Size, imageSizeType);
                    var canvasWidth = destinationSize.Width;
                    var canvasHeight = destinationSize.Height;
                    using (var newImage = fullsizeImage.GetThumbnailImage(canvasWidth, canvasHeight, null, IntPtr.Zero))
                    {
                        using (var resize = new MemoryStream())
                        {
                            using (var imageEncodeParameters = CreateImageEncoderParameters())
                            {
                                var destinationImageFormat = ImageFormat.Jpeg;
                                var imageEncoder = destinationImageFormat.GetEncoder();
                                newImage.Save(resize, imageEncoder, imageEncodeParameters);
                            }

                            return resize.ToArray();
                        }
                    }
                }
            }
        }

        private static EncoderParameters CreateImageEncoderParameters()
        {
            var imageEncoderParameters = new EncoderParameters(1);
            imageEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 75L);
            return imageEncoderParameters;
        }
    }
}