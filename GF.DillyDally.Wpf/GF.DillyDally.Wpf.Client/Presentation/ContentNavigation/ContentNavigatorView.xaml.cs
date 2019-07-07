using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für ContentNavigatorView.xaml
    /// </summary>
    public partial class ContentNavigatorView : IViewFor<ContentNavigatorViewModel>
    {
        public ContentNavigatorView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<ContentNavigatorViewModel> Members

        public ContentNavigatorViewModel ViewModel
        {
            get { return (ContentNavigatorViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion
    }
}