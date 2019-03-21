using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.RewardSystem;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    public sealed class ContentNavigatorController : ControllerBase<ContentNavigatorViewModel>
    {
        #region - Konstruktoren -

        public ContentNavigatorController(ContentNavigatorViewModel viewModel, IContentNavigator contentNavigator) :
            base(viewModel)
        {
            this._contentNavigator = contentNavigator;
        }

        #endregion

        #region - Methoden privat -

        protected override async Task OnInitializeAsync()
        {
            await Task.Run(() =>
            {
                this._currentContentController =
                    this._contentNavigator.Navigate(new AccountsControllerNavigationTarget());
                this.ViewModel.AssignDisplayTarget(this._currentContentController.ViewModel);
            });
        }

        #endregion

        #region - Felder privat -

        private readonly IContentNavigator _contentNavigator;
        private IController _currentContentController;

        #endregion
    }
}