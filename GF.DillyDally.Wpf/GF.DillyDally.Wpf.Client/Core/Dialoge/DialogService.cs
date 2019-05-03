using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal sealed class DialogService : IDialogService
    {
        private readonly ControllerFactory<DialogWindowController> _dialogWindowControllerFactory;
        private readonly DialogWindowFactory _dialogWindowFactory = new DialogWindowFactory();

        public DialogService(ControllerFactory<DialogWindowController> dialogWindowControllerFactory)
        {
            this._dialogWindowControllerFactory = dialogWindowControllerFactory;
        }

        #region IDialogService Members

        public async Task<IDialogResult> ShowDialogAsync(IController dialogContentController, IDialogSettings settings)
        {
            var completion = new TaskCompletionSource<IDialogResult>();
            var dialogWindowContentController = await this._dialogWindowControllerFactory.CreateControllerAsync();
            dialogWindowContentController.Content = dialogContentController.ViewModel;
            await Application.Current.Dispatcher.InvokeAsync(() =>
                completion.SetResult(this.ShowDialog(dialogWindowContentController, settings)));
            return await completion.Task;
        }

        #endregion

        public IDialogResult ShowDialog(DialogWindowController dialogWindowContentController, IDialogSettings settings)
        {
            var window =
                this._dialogWindowFactory.CreateDialogWindow(settings, dialogWindowContentController.ViewModel);

            var dialogWindowResultAssignCommand =
                this.CreateDialogConfirmationCommand(window, dialogWindowContentController);
            var wrappedDialogCommands = this.CreateDialogCommandsFrom(settings.DefaultDialogResult,
                settings.AvailableDialogResults, dialogWindowResultAssignCommand);
            dialogWindowContentController.DialogCommands = wrappedDialogCommands;
            var result = window.ShowDialog();
            return dialogWindowContentController.SelectedDialogResult;
        }

        private ICommand CreateDialogConfirmationCommand(Window window,
            DialogWindowController dialogWindowContentController)
        {
            var delegateCommand = new DelegateCommand<DialogCommandViewModel>(cmdVm =>
            {
                if (cmdVm.ValidateExecution())
                {
                    dialogWindowContentController.AssignResult(cmdVm);
                    window.DialogResult = true;
                }
            });

            return delegateCommand;
        }

        private IList<DialogCommandViewModel> CreateDialogCommandsFrom(IDialogResult defaultDialogResult,
            IReadOnlyList<IDialogResult> dialogResults, ICommand dialogWindowResultAssignCommand)
        {
            var dialogCommands = new List<DialogCommandViewModel>();
            var defaultDialogCommand =
                new DialogCommandViewModel(defaultDialogResult, dialogWindowResultAssignCommand, true);
            var otherDialogCommands = dialogResults.Except(defaultDialogResult.Yield())
                .Select(x => new DialogCommandViewModel(x, dialogWindowResultAssignCommand)).ToList();
            dialogCommands.Add(defaultDialogCommand);
            dialogCommands.AddRange(otherDialogCommands);
            return dialogCommands;
        }
    }
}