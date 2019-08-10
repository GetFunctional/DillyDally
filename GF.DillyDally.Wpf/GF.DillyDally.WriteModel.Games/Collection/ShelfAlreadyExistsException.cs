using System;

namespace GF.DillyDally.WriteModel.Games.Collection
{
    internal class ShelfAlreadyExistsException : Exception
    {
        public ShelfAlreadyExistsException(Guid shelfId)
        {
            
        }
    }
}