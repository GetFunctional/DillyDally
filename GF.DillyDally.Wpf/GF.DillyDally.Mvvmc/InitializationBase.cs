using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public abstract class InitializationBase
    {
        private readonly Subject<bool> _busyChanged = new Subject<bool>();

        private bool _isBusy;

        public IObservable<bool> WhenBusyChanged
        {
            get
            {
                return this._busyChanged;
            }
        }

        protected virtual async Task OnInitializeAsync()
        {
            await Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            this.IsBusy();
            await this.OnInitializeAsync();
            await this.OnInitializeCompletedAsync();
            this.IsReady();
        }

        protected virtual async Task OnInitializeCompletedAsync()
        {
            await Task.CompletedTask;
        }

        protected void IsBusy()
        {
            if (this._isBusy)
            {
                throw new InvalidOperationException();
            }

            this._isBusy = true;
            this._busyChanged.OnNext(this._isBusy);
        }

        protected void IsReady()
        {
            if (!this._isBusy)
            {
                throw new InvalidOperationException();
            }

            this._isBusy = true;
            this._busyChanged.OnNext(this._isBusy);
        }
    }
}