using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories.Events
{
    internal sealed class CategoryCreatedEvent : AggregateEventBase
    {
        public string Name { get; }
        public string ColorCode { get; }

        public CategoryCreatedEvent(Guid categoryId, string name, string colorCode) : base(categoryId)
        {
            this.Name = name;
            this.ColorCode = colorCode;
        }
    }
}