using System.Windows;
using GF.DillyDally.Wpf.Client.Core;

namespace GF.DillyDally.Wpf.Client
{
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this._bootstrapper = new Bootstrapper();
            this._bootstrapper.Run();

            var shell = new MainWindow();
            shell.ShowDialog();
        }
    }
}