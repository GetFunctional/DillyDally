using System;

namespace GF.DillyDally.Mvvmc
{
    public abstract class ControllerBase<TViewModel> : InitializationBase, IController<TViewModel>
        where TViewModel : IViewModel
    {
        protected ControllerBase(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }

        #region IController<TViewModel> Members

        IViewModel IController.ViewModel
        {
            get
            {
                return this.ViewModel;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool ConfirmClosing(object callSource)
        {
            return this.OnConfirmClosing(callSource);
        }

        public void Close()
        {
            this.OnClose();
            this.Dispose(true);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        protected virtual bool OnConfirmClosing(object callSource)
        {
            return true;
        }

        protected virtual void OnClose()
        {
        }
    }
}