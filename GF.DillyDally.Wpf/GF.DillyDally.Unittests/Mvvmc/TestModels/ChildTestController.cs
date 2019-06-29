using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Unittests.Mvvmc.TestModels
{
    internal class ChildTestController : ControllerBase<ChildViewModel>
    {
        public ChildTestController(ChildViewModel viewModel, ControllerFactory controllerFactory) : base(viewModel, controllerFactory)
        {
        }
    }
}