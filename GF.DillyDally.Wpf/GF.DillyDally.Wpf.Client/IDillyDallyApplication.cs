using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client
{
    public interface IDillyDallyApplication
    {
        Task<bool> NavigateInCurrentNavigatorAsync(INavigationTarget navigationTarget);
        void ShowOverlayDialog(IViewModel overlayContent);
        void ConfirmOverlayWith(IDialogResult result);
    }
}