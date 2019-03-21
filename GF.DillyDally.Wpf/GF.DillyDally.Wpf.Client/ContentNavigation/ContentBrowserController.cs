using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    public class ContentBrowserController : ControllerBase<ContentBrowserViewModel>
    {
        #region - Felder privat -

        private readonly ControllerFactory<ContentNavigatorController, ContentNavigatorViewModel>
            _contentNavigatorControllerFactory;
        private readonly IList<ContentNavigatorController> _navigatorControllers = new List<ContentNavigatorController>();

        #endregion

        #region - Konstruktoren -

        public ContentBrowserController(ContentBrowserViewModel viewModel,
            ControllerFactory<ContentNavigatorController, ContentNavigatorViewModel> contentNavigatorControllerFactory)
            : base(viewModel)
        {
            this._contentNavigatorControllerFactory = contentNavigatorControllerFactory;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var newNavigator = this._contentNavigatorControllerFactory.CreateController();
            this._navigatorControllers.Add(newNavigator);
            this.ViewModel.SelectCurrentNavigator(newNavigator.ViewModel);
        }

        #endregion
    }
}