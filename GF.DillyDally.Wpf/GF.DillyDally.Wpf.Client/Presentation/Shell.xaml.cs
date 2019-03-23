using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    /// <summary>
    ///     Interaktionslogik für Shell.xaml
    /// </summary>
    public partial class Shell : IViewFor<ShellViewModel>
    {
        #region Constructors

        #region - Konstruktoren -

        public Shell(ShellViewModel shellViewModel)
        {
            this.InitializeComponent();
            this.ViewModel = shellViewModel;
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public ShellViewModel ViewModel
        {
            get { return (ShellViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion

        #endregion
    }
}