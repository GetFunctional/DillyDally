using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    /// <summary>
    ///     Interaktionslogik für ContentNavigatorView.xaml
    /// </summary>
    public partial class ContentNavigatorView : IViewFor<ContentNavigatorViewModel>
    {
        #region - Konstruktoren -

        public ContentNavigatorView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region - Properties oeffentlich -

        public ContentNavigatorViewModel ViewModel
        {
            get { return (ContentNavigatorViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion
    }
}