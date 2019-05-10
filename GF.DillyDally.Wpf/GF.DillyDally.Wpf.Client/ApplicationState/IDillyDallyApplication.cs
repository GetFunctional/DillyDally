using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    public interface IDillyDallyApplication
    {
        Task<bool> NavigateInCurrentNavigatorAsync(INavigationTarget navigationTarget);
        void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings);
        void ConfirmOverlayWith(IDialogResult result);
    }
}