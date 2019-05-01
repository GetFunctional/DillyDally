using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Categories.Commands
{
    public sealed class CreateCategoryCommand : IRequest<CreateCategoryResponse>
    {
        public CreateCategoryCommand(string name, string colorCode)
        {
            this.Name = name;
            this.ColorCode = colorCode;
        }

        public string Name { get; }
        public string ColorCode { get; }
    }
}