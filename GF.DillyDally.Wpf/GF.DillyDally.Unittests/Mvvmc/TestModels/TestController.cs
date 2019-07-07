using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;

namespace GF.DillyDally.Unittests.Mvvmc.TestModels
{
    internal class TestController : DDControllerBase<TestViewModel>
    {
        internal int InitializationCount;

        public TestController(TestViewModel viewModel,IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this.ChildController = this.CreateChildController<ChildTestController>();
        }

        public ChildTestController ChildController { get; set; }

        public ChildTestController AnotherChildController { get; set; }

        protected override Task OnInitializeAsync()
        {
            this.InitializationCount++;
            return base.OnInitializeAsync();
        }

        internal void CreateAnotherChildController()
        {
            this.AnotherChildController = this.CreateChildController<ChildTestController>();
        }
    }
}