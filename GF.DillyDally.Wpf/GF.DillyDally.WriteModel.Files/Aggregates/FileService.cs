using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Files.Aggregates.Files.Commands;
using MediatR;

namespace GF.DillyDally.WriteModel.Files.Aggregates
{
    public class FileService : IFileService
    {
        private readonly IMediator _mediator;

        public FileService(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public Task<StoreFileResponse> GetOrCreateFileAsync(byte[] binary)
        {
            return this._mediator.Send(new StoreFileCommand(binary));
        }

        public Task<StoreFileResponse> GetOrCreateFileAsync(string filePath)
        {
            return this._mediator.Send(new StoreFileCommand(filePath));
        }
    }
}