using System;

namespace GF.DillyDally.Mvvmc
{
    public interface IMvvmcServiceFactory
    {
        IController GetController(Type controllerType);
    }
}