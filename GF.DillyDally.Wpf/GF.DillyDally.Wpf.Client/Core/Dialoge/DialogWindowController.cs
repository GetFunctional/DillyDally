using System.Collections.Generic;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal sealed class DialogWindowController : ControllerBase<DialogWindowViewModel>
    {
        public DialogWindowController(DialogWindowViewModel viewModel) : base(viewModel)
        {
        }

        public IViewModel Content
        {
            get { return this.ViewModel.Content; }
            set { this.ViewModel.Content = value; }
        }

        public IList<DialogCommandViewModel> DialogCommands
        {
            get { return this.ViewModel.DialogCommands; }
            set { this.ViewModel.DialogCommands = value; }
        }

        public IDialogResult SelectedDialogResult { get; private set; }

        public void AssignResult(DialogCommandViewModel dialogCommandViewModel)
        {
            this.SelectedDialogResult = dialogCommandViewModel.DialogResult;
        }
    }
}