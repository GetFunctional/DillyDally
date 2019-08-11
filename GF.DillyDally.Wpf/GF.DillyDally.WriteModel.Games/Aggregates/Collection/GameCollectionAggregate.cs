using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.WriteModel.Core.Aggregates;
using GF.DillyDally.WriteModel.Games.Aggregates.Collection.Events;
using GF.DillyDally.WriteModel.Games.Aggregates.Collection.Exceptions;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Collection
{
    internal class GameCollectionAggregate : AggregateRootBase
    {
        internal static Guid GameCollectionAggregateId = Guid.Parse("{5F422A53-BF10-4BFC-B59E-834EA67EAAE3}");

        public GameCollectionAggregate()
        {
            this.RegisterTransition<ShelfAddedToCollection>(this.Apply);
        }

        private GameCollectionAggregate(Guid collectionId)
        {
            this.AggregateId = collectionId;
        }

        private List<ShelfEntity> Shelves { get; } = new List<ShelfEntity>();

        private void Apply(ShelfAddedToCollection obj)
        {
            var newShelf = new ShelfEntity(obj.ShelfId);
            this.Shelves.Add(newShelf);
        }

        internal static GameCollectionAggregate Create()
        {
            return new GameCollectionAggregate(GameCollectionAggregateId);
        }

        internal void AddShelf(Guid shelfId)
        {
            if (this.Shelves.ToLookup(x => x.ShelfId).Contains(shelfId))
            {
                throw new ShelfAlreadyExistsException(shelfId);
            }

            this.RaiseEvent(new ShelfAddedToCollection(this.AggregateId, shelfId));
        }
    }
}