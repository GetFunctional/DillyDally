using System;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Mediation.Dialog
{
    internal class DialogRequest : INotification
    {
        public DialogRequest(IViewModel dialogContent) : this(dialogContent, new DialogSettings())
        {
        }

        public DialogRequest(IViewModel dialogContent, DialogSettings dialogSettings)
        {
            this.DialogSettings = dialogSettings ?? throw new ArgumentException(nameof(dialogSettings));
            this.DialogContent = dialogContent ?? throw new ArgumentException(nameof(dialogContent));
        }

        public IViewModel DialogContent { get; }

        public DialogSettings DialogSettings { get; }
    }
}