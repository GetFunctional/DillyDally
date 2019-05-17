using System;

namespace GF.DillyDally.Mvvmc
{
    public interface IController : IDisposable, ICloseAware
    {
        IViewModel ViewModel { get; }
        IObservable<bool> WhenBusyChanged { get; }

    }
}