using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Commands
{
    internal sealed class ActivityCommands
    {
        private readonly IControllerServices _controllerServices;

        public ActivityCommands(IControllerServices controllerServices)
        {
            this._controllerServices = controllerServices;
            this.CreateNewActivityCommand =
                controllerServices.ReactiveCommandFactory.CreateFromTask(this.CreateNewActivity);
        }

        public IReactiveCommand CreateNewActivityCommand { get; }

        private async Task CreateNewActivity()
        {
            using (var createActivityController =
                this._controllerServices.ControllerFactory.CreateAndInitializeController<CreateActivityController>())
            {
                await this._controllerServices.NavigationService.ShowDialogAsync(createActivityController);
            }
        }
    }
}