using System.Windows;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.ApplicationState;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Unittests.Core
{
    internal class UnittestApplicationRuntime : IApplicationRuntime
    {
        public void AddDataTemplate(object key, DataTemplate dataTemplate)
        {
            
        }

        public IController NavigateInCurrentNavigator(INavigationTarget navigationTarget)
        {
            return null;
        }

        public void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings)
        {
        }

        public void ConfirmOverlayWith(IDialogResult result)
        {
        }

        public object TryFindResource(DataTemplateKey key)
        {
            return null;
        }
    }
}