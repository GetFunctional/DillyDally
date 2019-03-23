using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für ContentNavigatorView.xaml
    /// </summary>
    public partial class ContentNavigatorView : IViewFor<ContentNavigatorViewModel>
    {
        #region Constructors

        #region - Konstruktoren -

        public ContentNavigatorView()
        {
            this.InitializeComponent();
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public ContentNavigatorViewModel ViewModel
        {
            get { return (ContentNavigatorViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion

        #endregion
    }
}