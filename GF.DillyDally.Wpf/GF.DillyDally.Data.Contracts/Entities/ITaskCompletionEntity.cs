using System;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface ITaskCompletionEntity
    {
        TaskCompletionKey TaskCompletionKey { get; }

        TaskKey TaskKey { get; set; }

        DateTime CompletedOn { get; set; }
    }
}