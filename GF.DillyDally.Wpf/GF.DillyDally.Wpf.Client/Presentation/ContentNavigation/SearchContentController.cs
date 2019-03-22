using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public class SearchContentController : ControllerBase<SearchContentViewModel>
    {
        #region - Felder privat -

        private readonly INavigationTargetProvider _navigationTargetProvider;

        #endregion

        #region - Konstruktoren -

        public SearchContentController(INavigationTargetProvider navigationTargetProvider) : base(
            new SearchContentViewModel(CreateNavigationTargetsFrom(navigationTargetProvider)))
        {
            this._navigationTargetProvider = navigationTargetProvider;
        }

        #endregion

        #region - Methoden privat -

        private static IList<NavigationTargetViewModel> CreateNavigationTargetsFrom(INavigationTargetProvider navigationTargetProvider)
        {
            return navigationTargetProvider.GetAllNavigationTargets().Select(nt => new NavigationTargetViewModel(nt.DisplayName, nt.NavigationTargetId)).ToList();
        }

        #endregion
    }
}