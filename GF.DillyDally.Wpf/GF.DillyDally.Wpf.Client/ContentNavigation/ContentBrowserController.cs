using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    public class ContentBrowserController : ControllerBase<ContentBrowserViewModel>
    {
        #region - Felder privat -

        private readonly ControllerFactory<ContentNavigatorController, ContentNavigatorViewModel> _contentNavigatorControllerFactory;

        #endregion

        #region - Konstruktoren -

        public ContentBrowserController(ContentBrowserViewModel viewModel,
            ControllerFactory<ContentNavigatorController, ContentNavigatorViewModel> contentNavigatorControllerFactory) : base(viewModel)
        {
            this._contentNavigatorControllerFactory = contentNavigatorControllerFactory;
        }

        #endregion
    }
}