using GF.DillyDally.Mvvmc.Contracts;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal class DialogConfirmed : INotification
    {
        public DialogConfirmed(IDialogResult dialogResult)
        {
            this.DialogResult = dialogResult;
        }

        public IDialogResult DialogResult { get; }
    }
}