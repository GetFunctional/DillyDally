using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    public interface IDillyDallyApplication
    {
        bool NavigateInCurrentNavigator(INavigationTarget navigationTarget);
        void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings);
        void ConfirmOverlayWith(IDialogResult result);
    }
}