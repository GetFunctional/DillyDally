using System;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Games.Collection
{
    internal class ShelfAddedEvent : AggregateEventBase
    {
        public ShelfAddedEvent(Guid aggregateId, Guid shelfId, int orderNumber) :
            base(aggregateId)
        {
            this.ShelfId = shelfId;
            this.OrderNumber = orderNumber;
        }

        public Guid ShelfId { get; }
        public int OrderNumber { get; }
    }
}