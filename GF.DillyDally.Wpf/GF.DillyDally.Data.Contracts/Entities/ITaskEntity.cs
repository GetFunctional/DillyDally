using System;
using System.ComponentModel.DataAnnotations;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface ITaskEntity
    {
        TaskKey TaskKey { get; }

        [StringLength(255)]
        string Name { get; set; }

        TaskType TaskType { get; set; }

        string Description { get; set; }

        DateTime? DueDate { get; set; }

        DateTime CreatedOn { get; set; }
    }
}