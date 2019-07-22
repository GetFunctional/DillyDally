using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Threading;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Mvvmc
{
    public abstract class ViewModelBase : ValidateableObject, IViewModel, IDisposable
    {
        private readonly Subject<ViewModelBusyChange> _viewModelBusyChanged = new Subject<ViewModelBusyChange>();
        private readonly IDisposable _busyObservable;
        private bool _isBusy;
        private string _isBusyMessage;

        protected ViewModelBase()
        {
            this._busyObservable = this._viewModelBusyChanged.Throttle(TimeSpan.FromMilliseconds(200)).Subscribe(this.HandleBusyChanged);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IViewModel Members

        public string IsBusyMessage
        {
            get
            {
                return this._isBusyMessage;
            }
            private set
            {
                this.SetAndRaiseIfChanged(ref this._isBusyMessage, value);
            }
        }

        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            private set
            {
                this.SetAndRaiseIfChanged(ref this._isBusy, value);
            }
        }

        #endregion

        private void HandleBusyChanged(ViewModelBusyChange payload)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                this.IsBusy = payload.IsBusy;
                this.IsBusyMessage = payload.IsBusyMessage;
            }));
        }

        public void MarkBusy(string busyMessage)
        {
            this._viewModelBusyChanged?.OnNext(new ViewModelBusyChange(true, busyMessage));
        }

        public void ClearBusy()
        {
            this._viewModelBusyChanged?.OnNext(new ViewModelBusyChange(false, null));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._viewModelBusyChanged?.Dispose();
                this._busyObservable?.Dispose();
            }
        }
    }
}