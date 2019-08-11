using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Files.Aggregates.Files.Filestore.Imageprocessing;

namespace GF.DillyDally.WriteModel.Files.Aggregates.Files.Filestore
{
    internal sealed class FileDataRepository
    {
        internal async Task<FileData> LoadFileAsync(IDbConnection connection, IGuidGenerator guidGenerator,
            FileRepository fileRepository,
            byte[] binary)
        {
            var fileBytes = binary;
            var md5HashString = new Md5HashGenerator().CreateMd5HashFromBinary(fileBytes);
            var fileName = Guid.NewGuid().ToString();
            var fileExtension = this.GetFileExtensionForDataFormat(fileBytes);

            return await this.GetFileData(connection, guidGenerator, fileRepository, md5HashString, fileBytes, fileName,
                fileExtension);
        }


        internal async Task<FileData> LoadFileAsync(IDbConnection connection, IGuidGenerator guidGenerator,
            FileRepository fileRepository,
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

            return await this.GetFileData(connection, guidGenerator, fileRepository, md5HashString, fileBytes, fileName,
                fileExtension);
        }

        private async Task<FileData> GetFileData(IDbConnection connection, IGuidGenerator guidGenerator,
            FileRepository fileRepository, string md5HashString,
            byte[] fileBytes, string fileName, string fileExtension)
        {
            if (!await fileRepository.HasFileWithSameHashAsync(connection, md5HashString))
            {
                var fileId = guidGenerator.GenerateGuid();
                return new FileData(fileId, true, ImageFormatDetector.GetImageFormat(fileBytes) != ImageFormat.Unknown,
                    fileBytes, md5HashString, fileBytes.LongLength, fileName, fileExtension);
            }

            var fileDataFromStore = await fileRepository.GetFileByMd5HashAsync(connection, md5HashString);
            return new FileData(fileDataFromStore.FileId, false,
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

            return imageFormat.GetFileExtensionForImageFormat().Replace("*", string.Empty);
        }
    }
}