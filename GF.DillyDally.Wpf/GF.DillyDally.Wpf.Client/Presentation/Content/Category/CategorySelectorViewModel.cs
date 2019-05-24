using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Category
{
    public class CategorySelectorViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryViewModel> _availableCategories;
        private CategoryViewModel _selectedCategory;

        public ObservableCollection<CategoryViewModel> AvailableCategories
        {
            get
            {
                return this._availableCategories;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._availableCategories, value);
            }
        }

        public CategoryViewModel SelectedCategory
        {
            get
            {
                return this._selectedCategory;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._selectedCategory, value);
            }
        }
    }
}