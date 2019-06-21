using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentBrowserController : ControllerBase<ContentBrowserViewModel>
    {
        private readonly ControllerFactory
            _controllerFactory;

        private readonly IList<ContentNavigatorController> _navigatorControllers =
            new List<ContentNavigatorController>();

        public ContentBrowserController(ContentBrowserViewModel viewModel,
            ControllerFactory controllerFactory)
            : base(viewModel)
        {
            this._controllerFactory = controllerFactory;
        }

        public bool NavigateInCurrentNavigator(INavigationTarget navigationTarget)
        {
            var currentActiveNavigator = this.ViewModel.CurrentActiveNavigator;
            if (currentActiveNavigator != null)
            {
                var controllerForViewModel =
                    this._navigatorControllers.Single(nc => nc.ViewModel == currentActiveNavigator);
                return controllerForViewModel.NavigateToTarget(navigationTarget);
            }

            return false;
        }

        protected override async Task OnInitializeAsync()
        {
            var newNavigator = this._controllerFactory.CreateController<ContentNavigatorController>();
            this._navigatorControllers.Add(newNavigator);
            this.ViewModel.SelectCurrentNavigator(newNavigator.ViewModel);

            await base.OnInitializeAsync();
        }
    }
}