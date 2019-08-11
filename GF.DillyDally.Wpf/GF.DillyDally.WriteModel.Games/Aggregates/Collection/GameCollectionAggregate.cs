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
            this.RegisterTransition<ShelfAdded>(this.Apply);
        }

        private GameCollectionAggregate(Guid collectionId)
        {
            this.AggregateId = collectionId;
        }
        
        private LinkedList<ShelfEntity> Shelves { get; } = new LinkedList<ShelfEntity>();


        private void Apply(ShelfAdded obj)
        {
            var wantsToBeFirst = obj.OrderNumber == 1 || this.Shelves.Count == 0;
            var wantsToBeLast = obj.OrderNumber >= this.Shelves.Count;

            var newShelf = new ShelfEntity(obj.ShelfId);

            if (wantsToBeFirst)
            {
                this.Shelves.AddFirst(newShelf);
            }

            if (wantsToBeLast)
            {
                this.Shelves.AddLast(newShelf);
            }
            else
            {
                var elementAbove = this.Shelves.Find(this.Shelves.Take(obj.OrderNumber - 1).Last());
                this.Shelves.AddAfter(elementAbove, newShelf);
            }
        }

        internal static GameCollectionAggregate Create()
        {
            return new GameCollectionAggregate(GameCollectionAggregateId);
        }

        internal void AddShelf(Guid shelfId, int orderNumber)
        {
            if (orderNumber < 0)
            {
                throw new ArgumentException();
            }
            
            if (this.Shelves.Any(s => s.ShelfId == shelfId))
            {
                throw new ShelfAlreadyExistsException(shelfId);
            }

            this.RaiseEvent(new ShelfAdded(this.AggregateId, shelfId, orderNumber));
        }
    }
}