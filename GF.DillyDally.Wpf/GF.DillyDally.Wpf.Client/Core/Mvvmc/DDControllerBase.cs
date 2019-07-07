using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Core.Mvvmc
{
    internal abstract class DDControllerBase<TViewModel> : ControllerBase<TViewModel> where TViewModel : IViewModel
    {
        private readonly HashSet<IDisposable> _additionalDisposables = new HashSet<IDisposable>();
        private readonly HashSet<IController> _childControllers;

        protected DDControllerBase(TViewModel viewModel, IControllerServices controllerServices) : base(viewModel)
        {
            this.ControllerServices = controllerServices;
            this._childControllers = new HashSet<IController>();
        }

        public IControllerServices ControllerServices { get; }

        protected IReadOnlyList<IController> ChildControllers
        {
            get { return this._childControllers.ToList(); }
        }

        protected void AddDisposable(IDisposable disposable)
        {
            this._additionalDisposables.Add(disposable);
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            await Task.WhenAll(this._childControllers.Select(ctrl => ctrl.InitializeAsync()));
        }

        protected override void OnClose()
        {
            base.OnClose();

            foreach (var childController in this._childControllers)
            {
                childController.Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                foreach (var additionalDisposable in this._additionalDisposables)
                {
                    additionalDisposable.Dispose();
                }

                foreach (var childController in this._childControllers)
                {
                    childController.Dispose();
                }
            }
        }

        protected override bool OnConfirmClosing(object callSource)
        {
            var confirmClosing = true;
            foreach (var childController in this._childControllers)
            {
                confirmClosing = confirmClosing && childController.ConfirmClosing(callSource);
            }

            return confirmClosing;
        }


        protected TController CreateChildController<TController>() where TController : IController
        {
            return (TController) this.CreateChildController(typeof(TController));
        }


        protected IController CreateChildController(Type controllerType)
        {
            IController controller;
            if (this.IsInitialized)
            {
                controller = this.ControllerServices.ControllerFactory.CreateAndInitializeController(controllerType);
            }
            else
            {
                controller = this.ControllerServices.ControllerFactory.CreateController(controllerType);
            }

            this._childControllers.Add(controller);
            return controller;
        }
    }
}