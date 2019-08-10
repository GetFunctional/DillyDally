using System;

namespace GF.DillyDally.WriteModel.Games.Collection
{
    internal class ShelfEntity
    {
        public Guid ShelfId { get; }

        public ShelfEntity(Guid shelfId)
        {
            this.ShelfId = shelfId;
        }
    }
}