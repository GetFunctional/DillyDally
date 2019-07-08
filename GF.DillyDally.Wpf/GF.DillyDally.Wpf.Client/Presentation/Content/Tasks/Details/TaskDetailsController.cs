using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Views.TaskDetails;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    internal sealed class TaskDetailsController : DDControllerBase<TaskDetailsViewModel>
    {
        private readonly ActivityContainerController _activityContainerController;
        private readonly TaskDetailDataConverter _taskDetailDataConverter = new TaskDetailDataConverter();

        public TaskDetailsController(TaskDetailsViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this._activityContainerController = this.CreateChildController<ActivityContainerController>();
            this._activityContainerController.DeactivateAddingNewActivities();

            this.ViewModel.ActivitiesViewModel = this._activityContainerController.ViewModel;
        }

        public async Task LoadTaskDetailsAsync(Guid taskId)
        {
            this.ViewModel.IsBusy = true;

            using (var connection = await this.ControllerServices.ReadModelStore.OpenConnectionAsync())
            {
                var taskDetailRepository = new TaskDetailsRepository();
                var taskDetailData = await taskDetailRepository.GetTaskDetailsAsync(connection, taskId);

                this.ApplyDataToViewModel(taskDetailData);

                this._activityContainerController.AssignActivities(
                    this._taskDetailDataConverter.ConvertToActivityItemViewModels(taskDetailData.TaskActivities));
            }


            this.ViewModel.IsBusy = false;
        }

        private void ApplyDataToViewModel(TaskDetailsEntity taskDetailData)
        {
            this.ViewModel.TaskName = taskDetailData.Name;
            this.ViewModel.DueDate = taskDetailData.DueDate;
            this.ViewModel.DefinitionOfDone = taskDetailData.DefinitionOfDone;
            this.ViewModel.Description = taskDetailData.Description;

            if (taskDetailData.TaskImages.Any(x => x.IsPreviewImage))
            {
                this.ViewModel.TaskPreviewImageBytes =
                    taskDetailData.TaskImages.First(x => x.IsPreviewImage).ImageBytesMedium;
            }
        }
    }
}