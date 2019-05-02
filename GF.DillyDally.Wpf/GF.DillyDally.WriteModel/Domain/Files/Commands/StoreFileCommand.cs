using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Files.Commands
{
    public sealed class StoreFileCommand : IRequest<StoreFileResponse>
    {
        public StoreFileCommand(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; }
    }
}