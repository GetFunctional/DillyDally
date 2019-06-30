using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Views.TaskDetails;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskDetailsController : ControllerBase<TaskDetailsViewModel>
    {
        private readonly ActivityContainerController _activityContainerController;
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly TaskDetailDataConverter _taskDetailDataConverter = new TaskDetailDataConverter();

        public TaskDetailsController(TaskDetailsViewModel viewModel, ControllerFactory controllerFactory,
            DatabaseFileHandler databaseFileHandler) : base(viewModel, controllerFactory)
        {
            this._databaseFileHandler = databaseFileHandler;
            this._activityContainerController = this.CreateChildController<ActivityContainerController>();
            this._activityContainerController.DeactivateAddingNewActivities();

            this.ViewModel.ActivitiesViewModel = this._activityContainerController.ViewModel;
        }

        public async Task LoadTaskDetailsAsync(Guid taskId)
        {
            this.ViewModel.IsBusy = true;

            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                var taskDetailRepository = new TaskDetailsRepository();
                var taskDetailData = await taskDetailRepository.GetTaskDetailsAsync(connection, taskId);

                this.ViewModel.TaskName = taskDetailData.Name;
                this.ViewModel.DueDate = taskDetailData.DueDate;
                this.ViewModel.DefinitionOfDone = taskDetailData.DefinitionOfDone;
                this.ViewModel.Description = taskDetailData.Description;

                this._activityContainerController.AssignActivities(
                    this._taskDetailDataConverter.ConvertToActivityItemViewModels(taskDetailData.TaskActivities));
            }


            this.ViewModel.IsBusy = false;
        }
    }
}