using System;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal sealed class DialogCommandResult : IDialogResult
    {
        public DialogCommandResult(string displayContent) : this(displayContent, () => true)
        {
        }

        public DialogCommandResult(string displayContent, Func<bool> dialogConfirmationCondition)
        {
            this.DialogConfirmationCondition = dialogConfirmationCondition;
            this.DisplayContent = displayContent;
        }

        #region IDialogResult Members

        public Func<bool> DialogConfirmationCondition { get; }

        public string DisplayContent { get; }

        #endregion
    }
}