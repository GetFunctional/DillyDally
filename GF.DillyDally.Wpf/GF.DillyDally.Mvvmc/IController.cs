using System;

namespace GF.DillyDally.Mvvmc
{
    public interface IController : IDisposable, ICloseAware
    {
        IViewModel ViewModel { get; }
    }
}