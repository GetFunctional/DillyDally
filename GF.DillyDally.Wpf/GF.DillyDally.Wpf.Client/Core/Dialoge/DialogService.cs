using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal sealed class DialogService : IDialogService
    {
        private readonly IDillyDallyApplication _dillyDallyApplication;

        public DialogService(IDillyDallyApplication dillyDallyApplication)
        {
            this._dillyDallyApplication = dillyDallyApplication;
        }

        #region IDialogService Members

        public async Task<IDialogResult> ShowDialogAsync(IDialogController dialogController)
        {
            var completion = new TaskCompletionSource<IDialogResult>();
            this._dillyDallyApplication.ShowOverlayDialog(dialogController.ViewModel);
            dialogController.WhenConfirmedResult.Subscribe(result =>
            {
                if (!dialogController.ConfirmClosing(this))
                {
                    return;
                }

                this._dillyDallyApplication.ConfirmOverlayWith(result);
                completion.SetResult(result);
                dialogController.Close();
            });
            return await completion.Task;
        }

        #endregion
    }
}