using System;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Collection.Exceptions
{
    internal class ShelfAlreadyExistsException : Exception
    {
        public ShelfAlreadyExistsException(Guid shelfId)
        {
            
        }
    }
}