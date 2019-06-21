using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Commands
{
    public sealed class ActivityCommands
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly IDialogService _dialogService;

        public ActivityCommands(ControllerFactory controllerFactory, IDialogService dialogService)
        {
            this._controllerFactory = controllerFactory;
            this._dialogService = dialogService;

            this.CreateNewActivityCommand = ReactiveCommand.CreateFromTask(this.CreateNewActivity);
        }

        public IReactiveCommand CreateNewActivityCommand { get; }

        private async Task CreateNewActivity()
        {
            var createActivityController = this._controllerFactory.CreateController<CreateActivityController>();
            using (createActivityController)
            {
                await this._dialogService.ShowDialogAsync(createActivityController);
            }
        }
    }
}