using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    /// <summary>
    ///     Interaktionslogik für DialogWindowView.xaml
    /// </summary>
    public partial class DialogWindowView : IViewFor<DialogWindowViewModel>
    {
        public DialogWindowView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<DialogWindowViewModel> Members

        public DialogWindowViewModel ViewModel
        {
            get { return (DialogWindowViewModel) this.DataContext; }
        }

        #endregion
    }
}