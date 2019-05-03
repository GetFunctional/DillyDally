using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    internal sealed class ShellController : ControllerBase<ShellViewModel>
    {
        private readonly ControllerFactory<ContentBrowserController> _browserControllerFactory;
        private ContentBrowserController _contentBrowserController;

        public ShellController(ShellViewModel viewModel,
            ControllerFactory<ContentBrowserController> browserControllerFactory) : base(
            viewModel)
        {
            this._browserControllerFactory = browserControllerFactory;
        }

        public async Task<bool> NavigateInCurrentNavigatorToAsync(INavigationTarget navigationTarget)
        {
            return await this._contentBrowserController.NavigateInCurrentNavigatorAsync(navigationTarget);
        }

        protected override async Task OnInitializeAsync()
        {
            this._contentBrowserController = await this._browserControllerFactory.CreateControllerAsync();
            this.ViewModel.ContentBrowserViewModel = this._contentBrowserController.ViewModel;
        }
    }
}