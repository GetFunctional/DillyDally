using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Files.Commands
{
    public sealed class CreateFileCommand : IRequest<CreateFileResponse>
    {
        public CreateFileCommand(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; }
    }
}