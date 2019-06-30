using System;
using System.Collections.Generic;

namespace GF.DillyDally.Mvvmc.Contracts
{
    public interface IDialogController : IController
    {
        IObservable<IDialogResult> WhenConfirmedResult { get; }

        IList<IDialogResult> AvailableResults { get; }
    }
}