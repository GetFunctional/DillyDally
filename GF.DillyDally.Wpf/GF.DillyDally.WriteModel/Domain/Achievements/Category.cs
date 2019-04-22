using System;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal class Category
    {
        public Guid CategoryId { get; }

        public Category(Guid categoryId)
        {
            this.CategoryId = categoryId;
        }
    }
}