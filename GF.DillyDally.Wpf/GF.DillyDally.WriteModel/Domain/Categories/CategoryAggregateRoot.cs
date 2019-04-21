using System;
using GF.DillyDally.WriteModel.Domain.Categories.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories
{
    internal sealed class CategoryAggregateRoot : AggregateRootBase
    {
        private CategoryAggregateRoot()
        {
            this.RegisterTransition<CategoryCreatedEvent>(this.Apply);
        }

        private CategoryAggregateRoot(Guid categoryId, string name, string colorCode) : this()
        {
            this.AggregateId = categoryId;
            this.Name = name;
            this.ColorCode = colorCode;

            this.RaiseEvent(new CategoryCreatedEvent(categoryId, name, colorCode));
        }

        public string Name { get; private set; }
        public string ColorCode { get; private set; }

        private void Apply(CategoryCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ColorCode = obj.ColorCode;
        }

        private bool ValidateColorCode(string colorCode)
        {
            return colorCode.StartsWith("#") && colorCode.Length == 7 || colorCode.Length == 9;
        }

        internal static IAggregateRoot Create(Guid categoryId, string name, string colorCode)
        {
            return new CategoryAggregateRoot(categoryId, name, colorCode);
        }
    }
}