using MediatR;

namespace GF.DillyDally.WriteModel.Files.Aggregates.Files.Commands
{
    internal sealed class StoreFileCommand : IRequest<StoreFileResponse>
    {
        public StoreFileCommand(string filePath)
        {
            this.FilePath = filePath;
        }

        public StoreFileCommand(byte[] binary)
        {
            this.Binary = binary;
        }

        public byte[] Binary { get; }
        public string FilePath { get; }
    }
}