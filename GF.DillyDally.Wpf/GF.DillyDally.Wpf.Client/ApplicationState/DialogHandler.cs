using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mediation.Dialog;
using MediatR;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    internal sealed class DialogHandler : INotificationHandler<DialogRequest>, INotificationHandler<DialogConfirmed>
    {
        private readonly IDillyDallyApplication _dillyDallyApplication;

        public DialogHandler(IDillyDallyApplication dillyDallyApplication)
        {
            this._dillyDallyApplication = dillyDallyApplication;
        }

        #region INotificationHandler<DialogConfirmed> Members

        public async Task Handle(DialogConfirmed notification, CancellationToken cancellationToken)
        {
            await Task.Yield();
            this._dillyDallyApplication.ConfirmOverlayWith(notification.DialogResult);
        }

        #endregion

        #region INotificationHandler<DialogRequest> Members

        public async Task Handle(DialogRequest request, CancellationToken cancellationToken)
        {
            await Task.Yield();
            this._dillyDallyApplication.ShowOverlayDialog(request.DialogContent);
        }

        #endregion
    }
}