using System;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Collection.Events
{
    internal class ShelfAdded : AggregateEventBase
    {
        public ShelfAdded(Guid aggregateId, Guid shelfId, int orderNumber) :
            base(aggregateId)
        {
            this.ShelfId = shelfId;
            this.OrderNumber = orderNumber;
        }

        public Guid ShelfId { get; }
        public int OrderNumber { get; }
    }
}