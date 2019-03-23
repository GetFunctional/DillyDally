using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für SearchContentView.xaml
    /// </summary>
    public partial class SearchContentView : IViewFor<SearchContentViewModel>
    {
        #region Constructors

        public SearchContentView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties, Indexers

        public SearchContentViewModel ViewModel
        {
            get { return (SearchContentViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion
    }
}