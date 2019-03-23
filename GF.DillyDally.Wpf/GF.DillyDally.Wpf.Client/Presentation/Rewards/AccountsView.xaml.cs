using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Rewards
{
    /// <summary>
    ///     Interaktionslogik für AccountsView.xaml
    /// </summary>
    public partial class AccountsView : IViewFor<AccountsViewModel>
    {
        public AccountsView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<AccountsViewModel> Members

        public AccountsViewModel ViewModel
        {
            get { return (AccountsViewModel) this.DataContext; }
        }

        #endregion
    }
}