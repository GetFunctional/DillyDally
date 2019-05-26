﻿using System;
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

        public Task Handle(DialogConfirmed notification, CancellationToken cancellationToken)
        {
            this._dillyDallyApplication.ConfirmOverlayWith(notification.DialogResult);
            return Task.CompletedTask;
        }

        #endregion

        #region INotificationHandler<DialogRequest> Members

        public Task Handle(DialogRequest request, CancellationToken cancellationToken)
        {
            this._dillyDallyApplication.ShowOverlayDialog(request.DialogContent, request.DialogSettings);
            return Task.CompletedTask;
        }

        #endregion
    }
}