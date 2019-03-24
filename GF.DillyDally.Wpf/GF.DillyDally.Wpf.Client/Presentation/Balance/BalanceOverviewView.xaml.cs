using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    /// <summary>
    ///     Interaktionslogik für BalanceOverviewView.xaml
    /// </summary>
    public partial class BalanceOverviewView : IViewFor<BalanceOverviewViewModel>
    {
        public BalanceOverviewView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<BalanceOverviewViewModel> Members

        public BalanceOverviewViewModel ViewModel
        {
            get { return (BalanceOverviewViewModel) this.DataContext; }
        }

        #endregion
    }
}