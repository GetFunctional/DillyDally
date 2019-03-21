using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client
{
    /// <summary>
    ///     Interaktionslogik für Shell.xaml
    /// </summary>
    public partial class Shell : IViewFor<ShellViewModel>
    {
        #region - Konstruktoren -

        public Shell(ShellViewModel shellViewModel)
        {
            this.InitializeComponent();
            this.ViewModel = shellViewModel;
        }

        #endregion

        #region - Properties oeffentlich -

        public ShellViewModel ViewModel
        {
            get { return (ShellViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion
    }
}