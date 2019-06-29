using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Unittests.Mvvmc.TestModels
{
    internal class TestController : ControllerBase<TestViewModel>
    {
        public TestController(TestViewModel viewModel, ControllerFactory controllerFactory) : base(viewModel, controllerFactory)
        {
            this.ChildController = this.CreateChildController<ChildTestController>();
        }

        public ChildTestController ChildController { get; set; }

        internal int InitializationCount = 0;

        protected override Task OnInitializeAsync()
        {
            this.InitializationCount++;
            return base.OnInitializeAsync();
        }

        internal void CreateAnotherChildController()
        {
            this.AnotherChildController = this.CreateChildController<ChildTestController>();
        }

        public ChildTestController AnotherChildController { get; set; }
    }
}