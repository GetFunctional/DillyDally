using System;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Commands
{
    public class CreateShelfResponse
    {
        public CreateShelfResponse(Guid newShelfId)
        {
            this.NewShelfId = newShelfId;
        }

        public Guid NewShelfId { get; }
    }
}