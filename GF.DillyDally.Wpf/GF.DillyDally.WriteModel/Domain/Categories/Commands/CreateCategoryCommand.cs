using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories.Commands
{
    public sealed class CreateCategoryCommand : AggregateCommandBase
    {
        public CreateCategoryCommand(string name, string colorCode) : base(Guid.Empty)
        {
            this.Name = name;
            this.ColorCode = colorCode;
        }

        public string Name { get; }
        public string ColorCode { get; }
    }
}