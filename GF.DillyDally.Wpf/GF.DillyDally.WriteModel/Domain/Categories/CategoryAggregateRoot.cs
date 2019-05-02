using System;
using GF.DillyDally.WriteModel.Domain.Categories.Events;
using GF.DillyDally.WriteModel.Domain.Lanes.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories
{
    internal sealed class CategoryAggregateRoot : AggregateRootBase
    {
        public CategoryAggregateRoot()
        {
            this.RegisterTransition<CategoryCreatedEvent>(this.Apply);
        }

        private CategoryAggregateRoot(Guid categoryId, Guid runningNumberId, string name, string colorCode) : this()
        {
            var creationEvent = new CategoryCreatedEvent(categoryId, runningNumberId, name, colorCode);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        public string Name { get; private set; }
        public string ColorCode { get; private set; }

        private void Apply(CategoryCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ColorCode = obj.ColorCode;
        }

        private static bool ValidateColorCode(string colorCode)
        {
            return colorCode.StartsWith("#") && colorCode.Length == 7 || colorCode.Length == 9;
        }

        internal static IAggregateRoot Create(Guid categoryId, Guid runningNumberId, string name, string colorCode)
        {
            if (!ValidateColorCode(colorCode))
            {
                throw new InvalidColorCodeGivenException(colorCode);
            }

            return new CategoryAggregateRoot(categoryId, runningNumberId, name, colorCode);
        }
    }
}