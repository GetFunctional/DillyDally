using System;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Collection.Events
{
    internal class ShelfAddedToCollection : AggregateEventBase
    {
        public ShelfAddedToCollection(Guid aggregateId, Guid shelfId) :
            base(aggregateId)
        {
            this.ShelfId = shelfId;
        }

        public Guid ShelfId { get; }
    }
}