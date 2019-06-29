using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.HeaderMenu
{
    internal sealed class HeaderMenuController : ControllerBase<HeaderMenuViewModel>
    {
        public HeaderMenuController(HeaderMenuViewModel viewModel, ControllerFactory controllerFactory) :
            base(viewModel,controllerFactory)
        {
        }
    }
}