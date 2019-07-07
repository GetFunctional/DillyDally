using GF.DillyDally.Wpf.Client.Core.DataTemplates;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    /// <summary>
    ///     Interaktionslogik für Shell.xaml
    /// </summary>
    public partial class Shell : IViewFor<ShellViewModel>
    {
        public Shell(ShellViewModel shellViewModel)
        {
            this.InitializeComponent();
            this.ViewModel = shellViewModel;
        }

        #region IViewFor<ShellViewModel> Members

        public ShellViewModel ViewModel
        {
            get { return (ShellViewModel) this.DataContext; }
            set { this.DataContext = value; }
        }

        #endregion
    }
}