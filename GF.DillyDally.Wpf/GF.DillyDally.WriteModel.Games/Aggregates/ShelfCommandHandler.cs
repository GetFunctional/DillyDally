using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Core;
using GF.DillyDally.WriteModel.Core.Aggregates;
using GF.DillyDally.WriteModel.Games.Aggregates.Collection;
using GF.DillyDally.WriteModel.Games.Aggregates.Commands;
using GF.DillyDally.WriteModel.Games.Aggregates.Shelves;
using MediatR;

namespace GF.DillyDally.WriteModel.Games.Aggregates
{
    internal class ShelfCommandHandler : CommandHandlerBase, IRequestHandler<CreateShelfCommand, CreateShelfResponse>
    {
        public ShelfCommandHandler(IAggregateRepository aggregateRepository, IMediator mediator) : base(
            aggregateRepository, mediator)
        {
        }

        #region IRequestHandler<CreateShelfCommand,CreateShelfResponse> Members

        public async Task<CreateShelfResponse> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
        {
            var collection = this.GetById<GameCollectionAggregate>(GameCollectionAggregate.GameCollectionAggregateId);
            var shelfFactory = new ShelfFactory();
            var newShelf = shelfFactory.CreateShelf(this.GenerateGuid(), request.ShelfName);

            collection.AddShelf(newShelf.AggregateId);

            await this.SaveAndDispatchAsync(newShelf);
            await this.SaveAndDispatchAsync(collection);

            return await Task.FromResult(new CreateShelfResponse(newShelf.AggregateId));
        }

        #endregion
    }
}