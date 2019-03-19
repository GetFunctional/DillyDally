using GF.DillyDally.Wpf.Client.Core;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für SearchContentView.xaml
    /// </summary>
    public partial class SearchContentView : IViewFor<SearchContentViewModel>
    {
        public SearchContentView()
        {
            this.InitializeComponent();
        }

        public SearchContentViewModel ViewModel
        {
            get { return (SearchContentViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }
    }
}