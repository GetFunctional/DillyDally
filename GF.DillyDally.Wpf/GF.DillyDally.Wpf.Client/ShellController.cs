using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.ContentNavigation;

namespace GF.DillyDally.Wpf.Client
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

        protected override void OnInitialize()
        {
            base.OnInitialize();

            this._contentBrowserController = this._browserControllerFactory.CreateController();

            this.ViewModel.ContentBrowserViewModel = this._contentBrowserController.ViewModel;
        }
    }
}