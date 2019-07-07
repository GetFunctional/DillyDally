using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal sealed class ContentBrowserController : DDControllerBase<ContentBrowserViewModel>
    {
        private readonly IList<ContentNavigatorController> _navigatorControllers =
            new List<ContentNavigatorController>();

        public ContentBrowserController(ContentBrowserViewModel viewModel,IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            var newNavigator = this.CreateChildController<ContentNavigatorController>();
            this._navigatorControllers.Add(newNavigator);
            this.ViewModel.SelectCurrentNavigator(newNavigator.ViewModel);
        }

        public IController NavigateInCurrentNavigator(INavigationTarget navigationTarget)
        {
            var currentActiveNavigator = this.ViewModel.CurrentActiveNavigator;
            if (currentActiveNavigator != null)
            {
                var controllerForViewModel =
                    this._navigatorControllers.Single(nc => nc.ViewModel == currentActiveNavigator);
                return controllerForViewModel.NavigateToTarget(navigationTarget);
            }

            return null;
        }
    }
}