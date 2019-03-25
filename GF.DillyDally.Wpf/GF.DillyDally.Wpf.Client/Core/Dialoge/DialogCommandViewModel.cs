using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public sealed class DialogCommandViewModel : ViewModelBase
    {
        public DialogCommandViewModel(IDialogResult dialogResult, ICommand dialogWindowResultAssignCommand,
            bool isDefault = false)
        {
            this.DialogResult = dialogResult;
            this.Command = dialogWindowResultAssignCommand;
            this.IsDefault = isDefault;
        }

        internal IDialogResult DialogResult { get; }

        public string Content
        {
            get { return this.DialogResult.DisplayContent; }
        }

        public bool IsDefault { get; }

        public ICommand Command { get; }

        public bool ValidateExecution()
        {
            return this.DialogResult.DialogConfirmationCondition();
        }
    }
}