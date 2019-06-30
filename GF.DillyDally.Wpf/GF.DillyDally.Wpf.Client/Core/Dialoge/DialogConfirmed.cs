using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
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