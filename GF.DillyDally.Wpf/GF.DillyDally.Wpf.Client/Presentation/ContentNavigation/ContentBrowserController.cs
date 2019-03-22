using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentBrowserController : ControllerBase<ContentBrowserViewModel>
    {
        #region - Felder privat -

        private readonly ControllerFactory<ContentNavigatorController>
            _contentNavigatorControllerFactory;

        private readonly IList<ContentNavigatorController> _navigatorControllers =
            new List<ContentNavigatorController>();

        #endregion

        #region - Konstruktoren -

        public ContentBrowserController(ContentBrowserViewModel viewModel,
            ControllerFactory<ContentNavigatorController> contentNavigatorControllerFactory)
            : base(viewModel)
        {
            this._contentNavigatorControllerFactory = contentNavigatorControllerFactory;
        }

        #endregion

        #region - Methoden oeffentlich -

        public bool NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget)
        {
            var currentActiveNavigator = this.ViewModel.CurrentActiveNavigator;
            if (currentActiveNavigator != null)
            {
                var controllerForViewModel = this._navigatorControllers.Single(nc => nc.ViewModel == currentActiveNavigator);
                return controllerForViewModel.NavigateToTarget(navigationTarget);
            }

            return false;
        }

        #endregion

        #region - Methoden privat -

        protected override async Task OnInitializeAsync()
        {
            var newNavigator = this._contentNavigatorControllerFactory.CreateController();
            this._navigatorControllers.Add(newNavigator);
            this.ViewModel.SelectCurrentNavigator(newNavigator.ViewModel);

            await base.OnInitializeAsync();
        }

        #endregion
    }
}