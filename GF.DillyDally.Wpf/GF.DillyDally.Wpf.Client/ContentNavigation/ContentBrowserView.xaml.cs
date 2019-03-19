using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    /// <summary>
    /// Interaktionslogik für ContentBrowserView.xaml
    /// </summary>
    public partial class ContentBrowserView : IViewFor<ContentBrowserViewModel>
    {
        #region - Konstruktoren -

        public ContentBrowserView()
        {
            InitializeComponent();
        }

        #endregion

        #region - Properties oeffentlich -

        public ContentBrowserViewModel ViewModel
        {
            get
            {
                return (ContentBrowserViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion
    }
}