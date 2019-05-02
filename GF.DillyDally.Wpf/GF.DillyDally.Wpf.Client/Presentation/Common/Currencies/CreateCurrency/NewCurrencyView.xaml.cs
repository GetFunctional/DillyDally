using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies.CreateCurrency
{
    /// <summary>
    ///     Interaktionslogik für NewCurrencyView.xaml
    /// </summary>
    public partial class NewCurrencyView : IViewFor<NewCurrencyViewModel>
    {
        public NewCurrencyView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<NewCurrencyViewModel> Members

        public NewCurrencyViewModel ViewModel
        {
            get
            {
                return (NewCurrencyViewModel)this.DataContext;
            }
        }

        #endregion
    }
}