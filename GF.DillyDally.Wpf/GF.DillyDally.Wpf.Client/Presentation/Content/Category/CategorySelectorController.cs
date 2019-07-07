using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Views.Selectors;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Category
{
    internal class CategorySelectorController : DDControllerBase<CategorySelectorViewModel>
    {
        public CategorySelectorController(CategorySelectorViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
        }

        protected override async Task OnInitializeAsync()
        {
            using (var connection = this.ControllerServices.ReadModelStore.OpenConnection())
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