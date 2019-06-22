using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Mediation.Dialog
{
    internal class DialogConfirmed : INotification
    {
        public IDialogResult DialogResult { get; }

        public DialogConfirmed(IDialogResult dialogResult)
        {
            this.DialogResult = dialogResult;
        }
    }
}