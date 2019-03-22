using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    internal sealed class ShellController : ControllerBase<ShellViewModel>
    {
        private readonly ControllerFactory<ContentBrowserController, ContentBrowserViewModel> _browserControllerFactory;
        private ContentBrowserController _contentBrowserController;

        public ShellController(ShellViewModel viewModel,
            ControllerFactory<ContentBrowserController, ContentBrowserViewModel> browserControllerFactory) : base(
            viewModel)
        {
            this._browserControllerFactory = browserControllerFactory;
        }
        
        protected override Task OnInitializeAsync()
        {
            this._contentBrowserController = this._browserControllerFactory.CreateController();
            this.ViewModel.ContentBrowserViewModel = this._contentBrowserController.ViewModel;

            return base.OnInitializeAsync();
        }

        public bool NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget)
        {
            return this._contentBrowserController.NavigateInCurrentNavigatorTo(navigationTarget);
        }
    }
}