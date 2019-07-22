using System;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Views.TaskDetails;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;
using GF.DillyDally.Wpf.Client.Presentation.Content.Images.Container;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    internal sealed class TaskDetailsController : DDControllerBase<TaskDetailsViewModel>
    {
        private readonly ActivityContainerController _activityContainerController;
        private readonly ActivityItemFactory _activityItemFactory = new ActivityItemFactory();
        private readonly ImageContainerController _imagesContainerController;

        private readonly TaskDetailsViewModelFactory _taskDetailsViewModelFactory = new TaskDetailsViewModelFactory();

        public TaskDetailsController(TaskDetailsViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this._activityContainerController = this.CreateChildController<ActivityContainerController>();
            this._activityContainerController.DeactivateAddingNewActivities();

            this._imagesContainerController = this.CreateChildController<ImageContainerController>();

            this.ViewModel.ReplaceActivityContainerTabItem(this._activityContainerController.ViewModel);
            this.ViewModel.ReplaceImageContainerTabItem(this._imagesContainerController.ViewModel);
        }

        public async Task LoadTaskDetailsAsync(Guid taskId)
        {
            this.ViewModel.IsBusy = true;

            using (var connection = await this.ControllerServices.ReadModelStore.OpenConnectionAsync())
            {
                var taskDetailRepository = new TaskDetailsRepository();
                var taskDetailData = await taskDetailRepository.GetTaskDetailsAsync(connection, taskId);

                this.ApplyDataToViewModel(taskDetailData);
            }

            this.ViewModel.IsBusy = false;
        }

        private void ApplyDataToViewModel(TaskDetailsEntity taskDetailData)
        {
            var taskSummaryViewModel = this._taskDetailsViewModelFactory.CreateTaskSummaryViewModel(taskDetailData);
            this.ViewModel.ReplaceTaskSummaryContainerTabItem(taskSummaryViewModel);

            var activities = new ActivityItemFactory().ConvertToActivityItemViewModels(taskDetailData.TaskActivities);
            this._activityContainerController.AssignActivities(activities);

            var taskImageViewModels = new ImageViewModelFactory().CreateViewModelFrom(taskDetailData.TaskImages);
            this._imagesContainerController.AssignImages(taskImageViewModels);
        }
    }
}