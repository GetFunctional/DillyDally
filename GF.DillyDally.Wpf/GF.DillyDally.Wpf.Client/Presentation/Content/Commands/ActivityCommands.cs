using System;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.Commands;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Commands
{
    internal sealed class ActivityCommands : IDisposable
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly NavigationService _navigationService;

        public ActivityCommands(ControllerFactory controllerFactory, IMediator mediator, ReactiveCommandFactory reactiveCommandFactory)
        {
            this._controllerFactory = controllerFactory;
            this._navigationService = new NavigationService(mediator);
            this.CreateNewActivityCommand = reactiveCommandFactory.CreateFromTask(this.CreateNewActivity);
        }

        public IReactiveCommand CreateNewActivityCommand { get; }

        #region IDisposable Members

        public void Dispose()
        {
            this.CreateNewActivityCommand?.Dispose();
        }

        #endregion

        private async Task CreateNewActivity()
        {
            using (var createActivityController = this._controllerFactory.CreateAndInitializeController<CreateActivityController>())
            {
                await this._navigationService.ShowDialogAsync(createActivityController);
            }
        }
    }
}