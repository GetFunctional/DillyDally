using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Category
{
    public class CategorySelectorViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryViewModel> _availableCategories;
        private CategoryViewModel _selectedCategory;

        public ObservableCollection<CategoryViewModel> AvailableCategories
        {
            get { return this._availableCategories; }
            set { this.SetAndRaiseIfChanged(ref this._availableCategories, value); }
        }

        public CategoryViewModel SelectedCategory
        {
            get { return this._selectedCategory; }
            set { this.SetAndRaiseIfChanged(ref this._selectedCategory, value); }
        }
    }
}