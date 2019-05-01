using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Files.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Files
{
    internal sealed class FileCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateFileCommand, CreateFileResponse>
    {
        private readonly FileRepository _fileRepository;

        public FileCommandHandler(IAggregateRepository aggregateRepository, FileRepository fileRepository) : base(
            aggregateRepository)
        {
            this._fileRepository = fileRepository;
        }

        #region IRequestHandler<CreateFileCommand,CreateFileResponse> Members

        public async Task<CreateFileResponse> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            // Load File
            var fileDataToStore = await this.LoadFileAsync(request.FilePath);

            if (fileDataToStore.IsNew)
            {
                // Store File first
                var fileId = this.GuidGenerator.GenerateGuid();
                //await this._fileRepository.InsertAsync()
            }


            return new CreateFileResponse();
            //var aggregate = FileAggregateRoot.Create(fileId, command.Name, command.ColorCode);
            //this._aggregateRepository.Save(aggregate);
            //return aggregate;
        }

        #endregion

        private async Task<Filedata> LoadFileAsync(string filePath)
        {
            var file = new FileInfo(filePath);

            if (!file.Exists)
            {
                throw new FileNotFoundException(filePath);
            }

            var md5HashString = CreateMd5HashFromFile(filePath);

            if (!await this._fileRepository.HasFileWithSameHashAsync(md5HashString))
            {
                var fileBytes = File.ReadAllBytes(filePath);
                return new Filedata(true, ImageFormatDetector.GetImageFormat(fileBytes) != ImageFormat.Unknown,
                    fileBytes, md5HashString, fileBytes.LongLength, file.Name, file.Extension);
            }

            var fileDataFromStore = await this._fileRepository.GetFileByMd5HashAsync(md5HashString);
            return new Filedata(false,
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