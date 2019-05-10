using GF.DillyDally.Wpf.Client.Core.Mediation.Dialog;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public sealed class DialogSettings
    {
        public DialogSettings() : this(DialogSize.Medium)
        {
        }

        private DialogSettings(DialogSize dialogSize)
        {
            this.DialogSize = dialogSize;
        }

        public DialogSize DialogSize { get; }
    }
}