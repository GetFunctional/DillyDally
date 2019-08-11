using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Games.Aggregates.Commands;
using MediatR;

namespace GF.DillyDally.WriteModel.Games.Aggregates
{
    internal class GameCollectionService : IGameCollectionService
    {
        private readonly IMediator _mediator;

        public GameCollectionService(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region IGameCollectionService Members

        public Task<CreateShelfResponse> CreateNewShelfAsync(string shelfName)
        {
            return this._mediator.Send(new CreateShelfCommand(shelfName));
        }

        #endregion
    }
}