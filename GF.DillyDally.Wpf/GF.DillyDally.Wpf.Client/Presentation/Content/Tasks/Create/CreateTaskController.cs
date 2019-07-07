using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;
using GF.DillyDally.Wpf.Client.Presentation.Content.Category;
using GF.DillyDally.WriteModel.Domain.Tasks;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    internal class CreateTaskController : DialogControllerBase<CreateTaskViewModel>
    {
        private readonly ActivityContainerController _activityContainerController;
        private readonly CategorySelectorController _categorySelectorController;
        private Guid? _presetLane;

        public CreateTaskController(CreateTaskViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this._categorySelectorController = this.CreateChildController<CategorySelectorController>();
            this._activityContainerController = this.CreateChildController<ActivityContainerController>();

            viewModel.CreateTaskCommand =
                controllerServices.CommandFactory.CreateFromTask(async () => await this.CompleteProcess());
            viewModel.CancelProcessCommand = controllerServices.CommandFactory.CreateFromAction(this.CancelProcess);

            viewModel.TaskAchievementsViewModel = new TaskAchievementsViewModel();
            viewModel.AddPage(new CreateTaskBasicInfosViewModel(this._categorySelectorController.ViewModel));
            viewModel.AddPage(new CreateTaskAdditionalInfosViewModel());
            viewModel.AddPage(new CreateTaskActivitiesViewModel(this._activityContainerController.ViewModel));
        }

        public IDialogResult CreateTaskDialogResult { get; } = new DialogCommandResult();
        public IDialogResult CancelDialogResult { get; } = new DialogCommandResult();

        private void CancelProcess()
        {
            this.ConfirmDialogWith(this.CancelDialogResult);
        }

        private async Task CompleteProcess()
        {
            this.ViewModel.IsBusy = true;

            if (this.IsInputValid(this.ViewModel))
            {
                var basicInfos = this.GetTaskBasicInfos();
                var taskName = basicInfos.TaskName;
                var category = basicInfos.SelectedCategory;
                var taskService = this.ControllerServices.GetDomainService<TaskService>();

                var task = await taskService.CreateNewTaskAsync(taskName, category.CategoryId, this._presetLane);

                var activities = this.GetTaskActivities();
                if (activities.Any())
                {
                    var activityIds = new HashSet<Guid>(activities.Select(x => x.ActivityId).Distinct());
                    await taskService.LinkTaskToActivitiesAsync(task.TaskId, activityIds);
                }

                this.ConfirmDialogWith(this.CreateTaskDialogResult);
            }

            this.ViewModel.IsBusy = false;
        }

        private IList<ActivityItemViewModel> GetTaskActivities()
        {
            var vmPage = this.ViewModel.GetPage<CreateTaskActivitiesViewModel>();

            if (vmPage.ActivityContainerViewModel.Activities.Any())
            {
                return vmPage.ActivityContainerViewModel.Activities;
            }

            return new List<ActivityItemViewModel>();
        }

        private CreateTaskBasicInfosViewModel GetTaskBasicInfos()
        {
            return this.ViewModel.GetPage<CreateTaskBasicInfosViewModel>();
        }

        private bool IsInputValid(CreateTaskViewModel viewModel)
        {
            var firstPage = viewModel.GetPage<CreateTaskBasicInfosViewModel>();
            return !string.IsNullOrWhiteSpace(firstPage.TaskName) && firstPage.SelectedCategory != null;
        }

        public void PresetLane(Guid laneId)
        {
            this._presetLane = laneId;
        }
    }
}