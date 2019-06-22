using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Commands
{
    public sealed class ActivityCommands
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly NavigationService _navigationService;

        public ActivityCommands(ControllerFactory controllerFactory, IMediator mediator)
        {
            this._controllerFactory = controllerFactory;
            this._navigationService = new NavigationService(mediator);

            this.CreateNewActivityCommand = ReactiveCommand.CreateFromTask(this.CreateNewActivity);
        }

        public IReactiveCommand CreateNewActivityCommand { get; }

        private async Task CreateNewActivity()
        {
            var createActivityController = this._controllerFactory.CreateController<CreateActivityController>();
            using (createActivityController)
            {
                await this._navigationService.ShowDialogAsync(createActivityController);
            }
        }
    }
}