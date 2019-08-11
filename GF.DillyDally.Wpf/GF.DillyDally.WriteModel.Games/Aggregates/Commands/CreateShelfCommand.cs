using MediatR;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Commands
{
    internal class CreateShelfCommand : IRequest<CreateShelfResponse>
    {
        public CreateShelfCommand(string shelfName)
        {
            this.ShelfName = shelfName;
        }

        public string ShelfName { get; }
    }
}