using System;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Shelves.Exceptions
{
    internal class GameAlreadyExistsOnShelfException : Exception
    {
        public GameAlreadyExistsOnShelfException(Guid gameGameId)
        {
        }
    }
}