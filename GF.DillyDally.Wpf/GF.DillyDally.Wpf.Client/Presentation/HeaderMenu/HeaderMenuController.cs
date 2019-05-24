using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;

namespace GF.DillyDally.Wpf.Client.Presentation.HeaderMenu
{
    internal sealed class HeaderMenuController : ControllerBase<HeaderMenuViewModel>
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly IDialogService _dialogService;

        public HeaderMenuController(HeaderMenuViewModel viewModel, IDialogService dialogService,
            ControllerFactory controllerFactory) : base(
            viewModel)
        {
            this._dialogService = dialogService;
            this._controllerFactory = controllerFactory;
        }
    }
}