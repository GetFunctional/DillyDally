using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationResponse
    {
        public NavigationResponse(bool successful, IController resolvedController)
        {
            this.Successful = successful;
            this.ResolvedController = resolvedController;
        }

        public bool Successful { get; }

        public IController ResolvedController { get; }
    }
}