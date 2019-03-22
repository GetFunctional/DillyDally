using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client
{
    public interface IDillyDallyApplication
    {
        bool NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget);
    }
}