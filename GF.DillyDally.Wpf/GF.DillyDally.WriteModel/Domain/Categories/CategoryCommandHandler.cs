using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories
{
    internal sealed class CategoryCommandHandler : CommandHandlerBase, ICommandHandler<CreateCategoryCommand>
    {
        #region ICommandHandler<CreateCategoryCommand> Members

        public IAggregateRoot Handle(CreateCategoryCommand command)
        {
            var categoryId = this.GuidGenerator.GenerateGuid();
            return CategoryAggregateRoot.Create(categoryId, command.Name, command.ColorCode);
        }

        #endregion
    }
}