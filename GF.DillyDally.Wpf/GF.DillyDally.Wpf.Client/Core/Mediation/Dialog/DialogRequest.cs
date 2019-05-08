using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Mediation.Dialog
{
    internal class DialogRequest : INotification
    {
        public DialogRequest(IViewModel dialogContent)
        {
            this.DialogContent = dialogContent;
        }

        public IViewModel DialogContent { get; }
    }
}