using System.Windows;
using DevExpress.Xpf.Core;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal sealed class DialogWindowFactory
    {
        internal Window CreateDialogWindow(IDialogSettings dialogSettings, DialogWindowViewModel viewModel)
        {
            var dialogWindow = new ThemedWindow();
            dialogWindow.DataContext = viewModel;
            dialogWindow.Content = viewModel;

            if (!double.IsNaN(dialogSettings.InitialDialogSize.Width) &&
                !double.IsNaN(dialogSettings.InitialDialogSize.Height))
            {
                dialogWindow.Width = dialogSettings.InitialDialogSize.Width;
                dialogWindow.Height = dialogSettings.InitialDialogSize.Height;
            }

            return dialogWindow;
        }
    }
}