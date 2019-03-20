﻿using System;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory
    {
        #region - Felder privat -

        private readonly MvvmcServiceFactory _mvvmcServiceFactory;

        #endregion

        #region - Konstruktoren -

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        #endregion

        #region - Methoden oeffentlich -

        public IController CreateController(Type controllerType)
        {
            var controller = this._mvvmcServiceFactory(controllerType);
            ((InitializationBase)controller).Initialize();
            ((InitializationBase)controller).InitializeAsync().ConfigureAwait(false);
            return (IController)controller;
        }

        #endregion
    }
}