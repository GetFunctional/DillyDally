using System;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface IOpenTaskEntity
    {
        TaskKey TaskKey { get; }

        string Name { get; }

        TaskType TaskType { get; }

        string Description { get; }

        DateTime? DueDate { get; }

        int RewardCount { get; }
    }
}