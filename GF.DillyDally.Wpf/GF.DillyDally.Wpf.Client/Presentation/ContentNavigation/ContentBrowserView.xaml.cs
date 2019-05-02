using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für ContentBrowserView.xaml
    /// </summary>
    public partial class ContentBrowserView : IViewFor<ContentBrowserViewModel>
    {
        public ContentBrowserView()
        {
            this.InitializeComponent();
        }

        #region IViewFor<ContentBrowserViewModel> Members

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