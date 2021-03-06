﻿using System;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Mvvmc
{
    public interface IMvvmcServiceFactory
    {
        IController GetController(Type controllerType);
    }
}