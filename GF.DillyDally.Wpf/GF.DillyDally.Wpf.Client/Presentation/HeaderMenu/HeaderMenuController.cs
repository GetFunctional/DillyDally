using GF.DillyDally.Wpf.Client.Core.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.HeaderMenu
{
    internal sealed class HeaderMenuController : DDControllerBase<HeaderMenuViewModel>
    {
        public HeaderMenuController(HeaderMenuViewModel viewModel,IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
        }
    }
}