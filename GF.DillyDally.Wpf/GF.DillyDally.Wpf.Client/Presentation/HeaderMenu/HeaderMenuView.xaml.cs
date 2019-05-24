using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.HeaderMenu
{
    /// <summary>
    ///     Interaktionslogik für HeaderMenuView.xaml
    /// </summary>
    public partial class HeaderMenuView : IViewFor<HeaderMenuViewModel>
    {
        public HeaderMenuView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<HeaderMenuViewModel> Members

        public HeaderMenuViewModel ViewModel
        {
            get
            {
                return (HeaderMenuViewModel)this.DataContext;
            }
        }

        #endregion
    }
}