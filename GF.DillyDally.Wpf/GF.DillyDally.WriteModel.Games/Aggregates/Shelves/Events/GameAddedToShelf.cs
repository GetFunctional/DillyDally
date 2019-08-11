using System;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Shelves.Events
{
    public class GameAddedToShelf : AggregateEventBase
    {
        public Guid GameId { get; }

        public GameAddedToShelf(Guid aggregateId, Guid gameId) : base(aggregateId)
        {
            this.GameId = gameId;
        }
    }
}