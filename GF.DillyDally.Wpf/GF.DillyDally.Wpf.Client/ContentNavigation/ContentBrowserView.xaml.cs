using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für ContentBrowserView.xaml
    /// </summary>
    public partial class ContentBrowserView : IViewFor<ContentBrowserViewModel>
    {
        #region - Konstruktoren -

        public ContentBrowserView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region - Properties oeffentlich -

        public ContentBrowserViewModel ViewModel
        {
            get { return (ContentBrowserViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion
    }
}