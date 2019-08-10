using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace GF.DillyDally.WriteModel.Files
{
    public static class ImageFormatDetector
    {
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            // see http://www.mikekunz.com/image_file_header.html  
            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var gif = Encoding.ASCII.GetBytes("GIF"); // GIF
            var png = new byte[] {137, 80, 78, 71}; // PNG
            var tiff = new byte[] {73, 73, 42}; // TIFF
            var tiff2 = new byte[] {77, 77, 42}; // TIFF
            var jpeg = new byte[] {255, 216, 255, 224}; // jpeg
            var jpeg2 = new byte[] {255, 216, 255, 225}; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
            {
                return ImageFormat.Bmp;
            }

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
            {
                return ImageFormat.Gif;
            }

            if (png.SequenceEqual(bytes.Take(png.Length)))
            {
                return ImageFormat.Png;
            }

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
            {
                return ImageFormat.Tiff;
            }

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
            {
                return ImageFormat.Tiff;
            }

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            {
                return ImageFormat.Jpeg;
            }

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
            {
                return ImageFormat.Jpeg;
            }

            return ImageFormat.Unknown;
        }

        public static ImageCodecInfo GetEncoder(this ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == GetNetImageFormat(format).Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        public static System.Drawing.Imaging.ImageFormat GetNetImageFormat(this ImageFormat imageFormat)
        {
            switch (imageFormat)
            {
                case ImageFormat.Unknown:
                    throw new NotSupportedException();
                case ImageFormat.Bmp:
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                case ImageFormat.Jpeg:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ImageFormat.Gif:
                    return System.Drawing.Imaging.ImageFormat.Gif;
                case ImageFormat.Tiff:
                    return System.Drawing.Imaging.ImageFormat.Tiff;
                case ImageFormat.Png:
                    return System.Drawing.Imaging.ImageFormat.Png;
                default:
                    throw new ArgumentOutOfRangeException(nameof(imageFormat), imageFormat, null);
            }
        }

        public static string GetFileExtensionForImageFormat(this ImageFormat format)
        {
            var netFormat = GetNetImageFormat(format);
            var extensions = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == netFormat.Guid)
                .FilenameExtension;
            var firstAvailableExtension = extensions.Split(';').First();
            return firstAvailableExtension;
        }
    }
}