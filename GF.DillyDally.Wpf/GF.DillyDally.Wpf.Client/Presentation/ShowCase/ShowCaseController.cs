﻿using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    internal class ShowCaseController : DDControllerBase<ShowCaseViewModel>
    {
        private readonly NavigationService _navigationService;

        public ShowCaseController(ShowCaseViewModel viewModel, NavigationService navigationService,
            IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this._navigationService = navigationService;
            this.ViewModel.TestDialogCommand = controllerServices.CommandFactory.CreateFromTask(this.ShowTestDialog);
        }

        private async Task ShowTestDialog()
        {
            var createTaskController = this.CreateChildController<CreateTaskController>();
            using (createTaskController)
            {
                await this._navigationService.ShowDialogAsync(createTaskController);
            }
        }
    }
}