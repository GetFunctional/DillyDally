using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.WriteModel.Core.Aggregates;
using GF.DillyDally.WriteModel.Games.Aggregates.Shelves.Events;
using GF.DillyDally.WriteModel.Games.Aggregates.Shelves.Exceptions;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Shelves
{
    internal class ShelfAggregate : AggregateRootBase
    {
        public ShelfAggregate()
        {
            this.RegisterTransition<ShelfCreated>(this.Apply);
            this.RegisterTransition<GameAddedToShelf>(this.Apply);
        }

        internal ShelfAggregate(Guid shelfId, string name)
        {
            this.AggregateId = shelfId;
            var creationEvent = new ShelfCreated(shelfId, name);
            this.RaiseEvent(creationEvent);
        }

        private List<GameEntity> Games { get; set; }
        private ShelfDetailsEntity ShelfDetails { get; set; }

        private void Apply(GameAddedToShelf obj)
        {
            this.Games.Add(new GameEntity(obj.GameId));
        }

        private void Apply(ShelfCreated obj)
        {
            this.AggregateId = obj.AggregateId;
            this.ShelfDetails = new ShelfDetailsEntity(obj.AggregateId, obj.Name);
            this.Games = new List<GameEntity>();
        }

        internal void AddGameToShelf(GameEntity game)
        {
            if (this.Games.ToLookup(x => x.GameId).Contains(game.GameId))
            {
                throw new GameAlreadyExistsOnShelfException(game.GameId);
            }

            var gameAddedToShelf = new GameAddedToShelf(this.AggregateId, game.GameId);
            this.RaiseEvent(gameAddedToShelf);
        }
    }
}