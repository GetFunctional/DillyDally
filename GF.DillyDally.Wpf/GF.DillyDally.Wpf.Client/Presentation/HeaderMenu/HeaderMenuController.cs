using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.HeaderMenu
{
    internal sealed class HeaderMenuController : ControllerBase<HeaderMenuViewModel>
    {
        private readonly ControllerFactory _controllerFactory;

        public HeaderMenuController(HeaderMenuViewModel viewModel, ControllerFactory controllerFactory) : base(
            viewModel)
        {
            this._controllerFactory = controllerFactory;
        }
    }
}