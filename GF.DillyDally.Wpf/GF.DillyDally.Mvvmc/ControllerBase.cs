using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public abstract class ControllerBase<TViewModel> : InitializationBase, IController<TViewModel>
        where TViewModel : IViewModel
    {
        private readonly List<IController> _childControllers;

        protected ControllerBase(TViewModel viewModel, ControllerFactory controllerFactory)
        {
            this.ViewModel = viewModel;
            this.ChildControllerFactory = controllerFactory;
            this._childControllers = new List<IController>();
        }

        protected IReadOnlyList<IController> ChildControllers
        {
            get { return this._childControllers; }
        }

        private ControllerFactory ChildControllerFactory { get; }

        public TViewModel ViewModel { get; }

        #region IController<TViewModel> Members

        IViewModel IController.ViewModel
        {
            get { return this.ViewModel; }
        }

        public void Dispose()
        {
            foreach (var childController in this._childControllers)
            {
                childController.Dispose();
            }

            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool ConfirmClosing(object callSource)
        {
            return this.OnConfirmClosing(callSource);
        }

        public void Close()
        {
            foreach (var childController in this._childControllers)
            {
                childController.Close();
            }

            this.OnClose();
            this.Dispose(true);
        }

        #endregion

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            await Task.WhenAll(this._childControllers.Select(ctrl => ctrl.InitializeAsync()));
        }

        protected IController CreateChildController(Type controllerType)
        {
            IController controller;
            if (this.IsInitialized)
            {
                controller = this.ChildControllerFactory.CreateAndInitializeController(controllerType);
            }
            else
            {
                controller = this.ChildControllerFactory.CreateController(controllerType);
            }

            this._childControllers.Add(controller);
            return controller;
        }

        protected TController CreateChildController<TController>() where TController : IController
        {
            return (TController) this.CreateChildController(typeof(TController));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        protected virtual bool OnConfirmClosing(object callSource)
        {
            var confirmClosing = true;
            foreach (var childController in this._childControllers)
            {
                confirmClosing = confirmClosing && childController.ConfirmClosing(callSource);
            }

            return confirmClosing;
        }

        protected virtual void OnClose()
        {
        }
    }
}