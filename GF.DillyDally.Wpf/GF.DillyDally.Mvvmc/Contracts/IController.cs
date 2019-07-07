using System;

namespace GF.DillyDally.Mvvmc.Contracts
{
    public interface IController : IDisposable, ICloseAware, INeedsInitialization
    {
        IViewModel ViewModel { get; }
    }
}