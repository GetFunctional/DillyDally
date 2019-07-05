using System;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Core.Mvvmc
{
    public interface IMvvmcServiceFactory
    {
        IController GetController(Type controllerType);
    }
}