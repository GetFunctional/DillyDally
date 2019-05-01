using System;

namespace GF.DillyDally.WriteModel.Domain.Categories.Commands
{
    public class CreateCategoryResponse
    {
        public CreateCategoryResponse(Guid categoryId)
        {
            this.CategoryId = categoryId;
        }

        public Guid CategoryId { get; }
    }
}