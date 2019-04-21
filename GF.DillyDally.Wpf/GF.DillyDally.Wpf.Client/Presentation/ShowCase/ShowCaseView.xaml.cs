using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    /// <summary>
    ///     Interaktionslogik für ShowCaseView.xaml
    /// </summary>
    public partial class ShowCaseView : IViewFor<ShowCaseViewModel>
    {
        public ShowCaseView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<ShowCaseViewModel> Members

        public ShowCaseViewModel ViewModel
        {
            get { return (ShowCaseViewModel) this.DataContext; }
        }

        #endregion
    }
}