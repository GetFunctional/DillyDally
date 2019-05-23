using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities.Commands
{
    public sealed class CreatePercentageActivityCommand : IRequest<CreatePercentageActivityResponse>
    {
        public CreatePercentageActivityCommand(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}