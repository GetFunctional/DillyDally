using System;
using System.Linq;
using System.Threading;
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

        public TaskDetailsController(TaskDetailsViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this._activityContainerController = this.CreateChildController<ActivityContainerController>();
            this._activityContainerController.DeactivateAddingNewActivities();

            this._imagesContainerController = this.CreateChildController<ImageContainerController>();

            this.ViewModel.ActivitiesViewModel = this._activityContainerController.ViewModel;
            this.ViewModel.ImagesContainerViewModel = this._imagesContainerController.ViewModel;
        }

        public async Task LoadTaskDetailsAsync(Guid taskId)
        {
            this.ViewModel.IsBusy = true;

            using (var connection = await this.ControllerServices.ReadModelStore.OpenConnectionAsync())
            {
                var taskDetailRepository = new TaskDetailsRepository();
                var taskDetailData = await taskDetailRepository.GetTaskDetailsAsync(connection, taskId);

                this.ApplyDataToViewModel(taskDetailData, this.ViewModel, this._imagesContainerController, this._activityContainerController);

               
            }


            this.ViewModel.IsBusy = false;
        }

        private void ApplyDataToViewModel(TaskDetailsEntity taskDetailData, TaskDetailsViewModel taskDetailsViewModel,
            ImageContainerController imageContainerController, ActivityContainerController activityContainerController)
        {
            taskDetailsViewModel.TaskName = taskDetailData.Name;
            taskDetailsViewModel.DueDate = taskDetailData.DueDate;
            taskDetailsViewModel.DefinitionOfDone = taskDetailData.DefinitionOfDone;
            taskDetailsViewModel.Description = taskDetailData.Description;

            if (taskDetailData.TaskImages.Any(x => x.IsPreviewImage))
            {
                taskDetailsViewModel.TaskPreviewImageBytes =
                    taskDetailData.TaskImages.First(x => x.IsPreviewImage).ImageBytesMedium;
            }

            var activities = new ActivityItemFactory().ConvertToActivityItemViewModels(taskDetailData.TaskActivities);
            activityContainerController.AssignActivities(activities);

            var taskImageViewModels = new ImageViewModelFactory().CreateViewModelFrom(taskDetailData.TaskImages);
            imageContainerController.AssignImages(taskImageViewModels);
        }
    }
}