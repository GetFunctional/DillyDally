using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities.Commands
{
    internal sealed class CanCreateActivityCommand : IRequest<CanCreateActivityResponse>
    {
        public CanCreateActivityCommand(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}