using System;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Shelves
{
    internal class ShelfDetailsEntity
    {
        public Guid ShelfId { get; }
        public string Name { get; }

        public ShelfDetailsEntity(Guid shelfId, string name)
        {
            this.ShelfId = shelfId;
            this.Name = name;
        }
    }
}