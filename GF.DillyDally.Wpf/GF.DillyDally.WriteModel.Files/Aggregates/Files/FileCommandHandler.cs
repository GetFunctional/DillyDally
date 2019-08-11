using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Core;
using GF.DillyDally.WriteModel.Core.Aggregates;
using GF.DillyDally.WriteModel.Files.Aggregates.Files.Commands;
using GF.DillyDally.WriteModel.Files.Aggregates.Files.Filestore;
using MediatR;

namespace GF.DillyDally.WriteModel.Files.Aggregates.Files
{
    internal sealed class FileCommandHandler : CommandHandlerBase,
        IRequestHandler<StoreFileCommand, StoreFileResponse>
    {
        private readonly IDbConnectionFactory _writeModelStore;

        public FileCommandHandler(IAggregateRepository aggregateRepository, IDbConnectionFactory writeModelStore,
            IMediator mediator) : base(
            aggregateRepository, mediator)
        {
            this._writeModelStore = writeModelStore;
        }

        #region IRequestHandler<StoreFileCommand,StoreFileResponse> Members

        public async Task<StoreFileResponse> Handle(StoreFileCommand request, CancellationToken cancellationToken)
        {
            using (var connection = this._writeModelStore.OpenConnection())
            {
                return await this.GetOrCreateFileAsync(request, this.GuidGenerator, connection);
            }
        }

        #endregion

        internal async Task<StoreFileResponse> GetOrCreateFileAsync(StoreFileCommand request,
            IGuidGenerator guidGenerator, IDbConnection connection)
        {
            // Load File
            var fileAggregateFactory = new FileAggregateFactory();
            var fileRepository = new FileRepository();
            var fileDataRepository = new FileDataRepository();
            FileData fileDataToStore;
            if (request.FilePath != null)
            {
                fileDataToStore =
                    await fileDataRepository.LoadFileAsync(connection, guidGenerator, fileRepository, request.FilePath);
            }
            else if (request.Binary != null)
            {
                fileDataToStore =
                    await fileDataRepository.LoadFileAsync(connection, guidGenerator, fileRepository, request.Binary);
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

                var aggregate = fileAggregateFactory.CreateFile(fileDataToStore);

                await this.SaveAndDispatchAsync(aggregate);

                return new StoreFileResponse(fileDataToStore.FileId, false);
            }


            return new StoreFileResponse(fileDataToStore.FileId, true);
        }
    }
}