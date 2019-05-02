using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.Shared.Images
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

            var rnd = Math.Min(maxWidth / (decimal)src.Width, maxHeight / (decimal)src.Height);
            return new Size((int)Math.Round(src.Width * rnd), (int)Math.Round(src.Height * rnd));
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
                            var destinationImageFormat = ImageFormatDetector.GetImageFormat(sourceBytes);
                            switch (destinationImageFormat)
                            {
                                case ImageFormat.Unknown:
                                    throw new NotImplementedException();
                                case ImageFormat.Bmp:
                                    newImage.Save(resize, System.Drawing.Imaging.ImageFormat.Bmp);
                                    break;
                                case ImageFormat.Jpeg:
                                    newImage.Save(resize, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    break;
                                case ImageFormat.Gif:
                                    newImage.Save(resize, System.Drawing.Imaging.ImageFormat.Gif);
                                    break;
                                case ImageFormat.Tiff:
                                    newImage.Save(resize, System.Drawing.Imaging.ImageFormat.Tiff);
                                    break;
                                case ImageFormat.Png:
                                    newImage.Save(resize, System.Drawing.Imaging.ImageFormat.Png);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(nameof(destinationImageFormat), destinationImageFormat, null);
                            }

                            return resize.ToArray();
                        }
                    }
                }
            }
        }
    }
}