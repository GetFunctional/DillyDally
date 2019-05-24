using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities.Commands
{
    public sealed class CreatePercentageActivityCommand : IRequest<CreatePercentageActivityResponse>
    {
        public CreatePercentageActivityCommand(string name, byte[] previewImageBytes = null)
        {
            this.Name = name;
            this.PreviewImageBytes = previewImageBytes;
        }

        public string Name { get; }
        public byte[] PreviewImageBytes { get; }
    }
}