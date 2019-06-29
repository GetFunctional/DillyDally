using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Views.Selectors;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Category
{
    public class CategorySelectorController : ControllerBase<CategorySelectorViewModel>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public CategorySelectorController(CategorySelectorViewModel viewModel, DatabaseFileHandler databaseFileHandler, ControllerFactory controllerFactory) :
            base(viewModel,controllerFactory)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        protected override async Task OnInitializeAsync()
        {
            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                var categorySelectorRepository = new CategorySelectorRepository();
                var categories = await categorySelectorRepository.GetAllCategoriesAsync(connection);

                var categoryViewModels = this.MapToCategoryViewModels(categories);
                this.ViewModel.AvailableCategories = new ObservableCollection<CategoryViewModel>(categoryViewModels);
            }
        }

        private IList<CategoryViewModel> MapToCategoryViewModels(IList<CategorySelectorEntity> categories)
        {
            return categories.Select(x => new CategoryViewModel(x.Name, x.CategoryId)).ToList();
        }
    }
}