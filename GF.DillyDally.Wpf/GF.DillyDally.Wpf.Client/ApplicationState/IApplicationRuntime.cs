using System.Windows;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    public interface IApplicationRuntime
    {
        void AddDataTemplate(object key, DataTemplate dataTemplate);

        IController NavigateInCurrentNavigator(INavigationTarget navigationTarget);
        void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings);
        void ConfirmOverlayWith(IDialogResult result);
        object TryFindResource(DataTemplateKey key);
    }
}