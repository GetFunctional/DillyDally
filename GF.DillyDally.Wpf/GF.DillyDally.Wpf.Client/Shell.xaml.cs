using System.Windows;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client
{
    /// <summary>
    ///     Interaktionslogik für Shell.xaml
    /// </summary>
    public partial class Shell : Window, IViewFor<ShellViewModel>
    {
        public Shell(ShellViewModel shellViewModel)
        {
            this.InitializeComponent();
            this.ViewModel = shellViewModel;
        }

        public ShellViewModel ViewModel
        {
            get => (ShellViewModel) this.DataContext;
            set => this.DataContext = value;
        }
    }
}