using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    /// <summary>
    ///     Interaktionslogik für CurrenciesOverviewView.xaml
    /// </summary>
    public partial class CurrenciesOverviewView : IViewFor<CurrenciesOverviewViewModel>
    {
        public CurrenciesOverviewView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<CurrenciesOverviewViewModel> Members

        public CurrenciesOverviewViewModel ViewModel
        {
            get
            {
                return (CurrenciesOverviewViewModel)this.DataContext;
            }
        }

        #endregion
    }
}