using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Shared.Images;

namespace GF.DillyDally.WriteModel.Domain.Files
{
    internal sealed class FileDataRepository
    {
        internal async Task<Filedata> LoadFileAsync(IDbConnection connection, IGuidGenerator guidGenerator, FileRepository fileRepository,
            byte[] binary)
        {
            var fileBytes = binary;
            var md5HashString = new Md5HashGenerator().CreateMd5HashFromBinary(fileBytes);
            var fileName = Guid.NewGuid().ToString();
            var fileExtension = this.GetFileExtensionForDataFormat(fileBytes);

            return await this.GetFileData(connection, guidGenerator, fileRepository, md5HashString, fileBytes, fileName, fileExtension);
        }


        internal async Task<Filedata> LoadFileAsync(IDbConnection connection, IGuidGenerator guidGenerator, FileRepository fileRepository,
            string filePath)
        {
            var file = new FileInfo(filePath);

            if (!file.Exists)
            {
                throw new FileNotFoundException(filePath);
            }

            var fileBytes = File.ReadAllBytes(filePath);
            var md5HashString = new Md5HashGenerator().CreateMd5HashFromFile(filePath);
            var fileName = file.Name;
            var fileExtension = file.Extension;

            return await this.GetFileData(connection, guidGenerator, fileRepository, md5HashString, fileBytes, fileName, fileExtension);
        }

        private async Task<Filedata> GetFileData(IDbConnection connection, IGuidGenerator guidGenerator, FileRepository fileRepository, string md5HashString,
            byte[] fileBytes, string fileName, string fileExtension)
        {
            if (!await fileRepository.HasFileWithSameHashAsync(connection, md5HashString))
            {
                var fileId = guidGenerator.GenerateGuid();
                return new Filedata(fileId, true, ImageFormatDetector.GetImageFormat(fileBytes) != ImageFormat.Unknown,
                    fileBytes, md5HashString, fileBytes.LongLength, fileName, fileExtension);
            }

            var fileDataFromStore = await fileRepository.GetFileByMd5HashAsync(connection, md5HashString);
            return new Filedata(fileDataFromStore.FileId, false,
                ImageFormatDetector.GetImageFormat(fileDataFromStore.Binary) != ImageFormat.Unknown,
                fileDataFromStore.Binary, fileDataFromStore.Md5Hash, fileDataFromStore.Size, fileDataFromStore.Name,
                fileDataFromStore.Extension);
        }

        private string GetFileExtensionForDataFormat(byte[] fileBytes)
        {
            var imageFormat = ImageFormatDetector.GetImageFormat(fileBytes);

            if (imageFormat == ImageFormat.Unknown)
            {
                throw new NotImplementedException();
            }

            return ImageFormatDetector.GetFileExtensionForImageFormat(imageFormat);
        }
    }
}