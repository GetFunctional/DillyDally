using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories.Events
{
    public sealed class CategoryCreatedEvent : AggregateEventBase
    {
        public CategoryCreatedEvent(Guid categoryId, Guid runningNumberId, string name, string colorCode) : base(categoryId)
        {
            this.RunningNumberId = runningNumberId;
            this.Name = name;
            this.ColorCode = colorCode;
        }

        public string Name { get; }
        public string ColorCode { get; }
        public Guid RunningNumberId { get; }
    }
}