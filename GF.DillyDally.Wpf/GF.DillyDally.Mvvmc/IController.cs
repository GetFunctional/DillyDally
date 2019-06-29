using System;

namespace GF.DillyDally.Mvvmc
{
    public interface IController : IDisposable, ICloseAware, INeedsInitialization
    {
        IViewModel ViewModel { get; }

    }
}