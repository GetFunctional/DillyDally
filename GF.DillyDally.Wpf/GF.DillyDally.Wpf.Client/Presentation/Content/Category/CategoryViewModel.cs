using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Category
{
    public class CategoryViewModel : ViewModelBase
    {
        public CategoryViewModel(string categoryName, Guid categoryId)
        {
            this.CategoryName = categoryName;
            this.CategoryId = categoryId;
        }

        public string CategoryName { get; }
        public Guid CategoryId { get; }
    }
}