using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Files.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Files
{
    internal sealed class FileCommandHandler : CommandHandlerBase,
        IRequestHandler<StoreFileCommand, StoreFileResponse>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public FileCommandHandler(IAggregateRepository aggregateRepository, DatabaseFileHandler databaseFileHandler) : base(
            aggregateRepository)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region IRequestHandler<StoreFileCommand,StoreFileResponse> Members

        public async Task<StoreFileResponse> Handle(StoreFileCommand request, CancellationToken cancellationToken)
        {
            using (var connection = this._databaseFileHandler.OpenConnection())
            {
                return await GetOrCreateFileAsync(request, this.AggregateRepository, connection, this.GuidGenerator, new FileRepository());
            }
        }

        #endregion

        internal static async Task<StoreFileResponse> GetOrCreateFileAsync(StoreFileCommand request, IAggregateRepository aggregateRepository,
            IDbConnection connection, IGuidGenerator guidGenerator, FileRepository fileRepository)
        {
            // Load File
            var fileDataToStore = await LoadFileAsync(connection, guidGenerator, fileRepository, request.FilePath);

            if (fileDataToStore.IsNew)
            {
                // Store File first
                await fileRepository.InsertAsync(connection, new FileEntity
                                                             {
                                                                 Binary = fileDataToStore.Binary,
                                                                 Extension = fileDataToStore.Extension,
                                                                 FileId = fileDataToStore.FileId,
                                                                 Md5Hash = fileDataToStore.Md5Hash,
                                                                 Name = fileDataToStore.Name,
                                                                 Size = fileDataToStore.Size,
                                                                 IsImage = fileDataToStore.IsImage
                                                             });

                var aggregate = FileAggregateRoot.Create(fileDataToStore.FileId, fileDataToStore.Name, fileDataToStore.Size, fileDataToStore.Md5Hash,
                    fileDataToStore.Extension);
                aggregateRepository.Save(aggregate);

                return new StoreFileResponse(fileDataToStore.FileId, false);
            }


            return new StoreFileResponse(fileDataToStore.FileId, true);
        }

        private static async Task<Filedata> LoadFileAsync(IDbConnection connection, IGuidGenerator guidGenerator, FileRepository fileRepository,
            string filePath)
        {
            var file = new FileInfo(filePath);

            if (!file.Exists)
            {
                throw new FileNotFoundException(filePath);
            }

            var md5HashString = CreateMd5HashFromFile(filePath);

            if (!await fileRepository.HasFileWithSameHashAsync(connection, md5HashString))
            {
                var fileId = guidGenerator.GenerateGuid();
                var fileBytes = File.ReadAllBytes(filePath);
                return new Filedata(fileId, true, ImageFormatDetector.GetImageFormat(fileBytes) != ImageFormat.Unknown,
                    fileBytes, md5HashString, fileBytes.LongLength, file.Name, file.Extension);
            }

            var fileDataFromStore = await fileRepository.GetFileByMd5HashAsync(connection, md5HashString);
            return new Filedata(fileDataFromStore.FileId, false,
                ImageFormatDetector.GetImageFormat(fileDataFromStore.Binary) != ImageFormat.Unknown,
                fileDataFromStore.Binary, fileDataFromStore.Md5Hash, fileDataFromStore.Size, fileDataFromStore.Name,
                fileDataFromStore.Extension);
        }

        private static string CreateMd5HashFromFile(string filePath)
        {
            string md5HashString;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var md5Hash = md5.ComputeHash(stream);
                    md5HashString = BitConverter.ToString(md5Hash).Replace("-", "").ToLowerInvariant();
                }
            }

            return md5HashString;
        }
    }
}