using System;
using System.Data;
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
                return await GetOrCreateFileAsync(request, this.AggregateRepository, connection, this.GuidGenerator);
            }
        }

        #endregion

        internal static async Task<StoreFileResponse> GetOrCreateFileAsync(StoreFileCommand request, IAggregateRepository aggregateRepository,
            IDbConnection connection, IGuidGenerator guidGenerator)
        {
            // Load File
            var fileRepository = new FileRepository();
            var fileDataRepository = new FileDataRepository();
            Filedata fileDataToStore;
            if (request.FilePath != null)
            {
                fileDataToStore = await fileDataRepository.LoadFileAsync(connection, guidGenerator, fileRepository, request.FilePath);
            }
            else if (request.Binary != null)
            {
                fileDataToStore = await fileDataRepository.LoadFileAsync(connection, guidGenerator, fileRepository, request.Binary);
            }
            else
            {
                throw new ArgumentException(nameof(request));
            }

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
    }
}