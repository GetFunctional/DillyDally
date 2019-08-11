using System;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Shelves.Events
{
    public sealed class ShelfCreated : AggregateEventBase
    {
        public ShelfCreated(Guid aggregateId, string name) : base(aggregateId)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}