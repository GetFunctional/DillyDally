using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für ContentBrowserView.xaml
    /// </summary>
    public partial class ContentBrowserView : IViewFor<ContentBrowserViewModel>
    {
        #region Constructors

        #region - Konstruktoren -

        public ContentBrowserView()
        {
            this.InitializeComponent();
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public ContentBrowserViewModel ViewModel
        {
            get { return (ContentBrowserViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion

        #endregion
    }
}