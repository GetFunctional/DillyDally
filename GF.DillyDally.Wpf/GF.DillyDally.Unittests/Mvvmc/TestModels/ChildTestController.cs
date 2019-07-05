using GF.DillyDally.Wpf.Client.Core.Mvvmc;

namespace GF.DillyDally.Unittests.Mvvmc.TestModels
{
    internal class ChildTestController : DDControllerBase<ChildViewModel>
    {
        public ChildTestController(ChildViewModel viewModel,ControllerFactory controllerFactory)
            : base(viewModel, controllerFactory)
        {
        }
    }
}