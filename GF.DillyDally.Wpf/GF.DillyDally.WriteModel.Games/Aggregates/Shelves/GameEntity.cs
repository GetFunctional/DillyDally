using System;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Shelves
{
    internal sealed class GameEntity
    {
        public GameEntity(Guid gameId)
        {
            this.GameId = gameId;
        }

        public Guid GameId { get; }
    }
}