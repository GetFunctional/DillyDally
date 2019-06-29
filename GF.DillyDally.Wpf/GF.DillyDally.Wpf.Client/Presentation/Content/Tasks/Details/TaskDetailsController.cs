using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Views.TaskBoard;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskDetailsController : ControllerBase<TaskDetailsViewModel>
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly DatabaseFileHandler _databaseFileHandler;
        private ActivityContainerController _activityContainerController;

        public TaskDetailsController(TaskDetailsViewModel viewModel, ControllerFactory controllerFactory, DatabaseFileHandler databaseFileHandler) : base(viewModel,controllerFactory)
        {
            this._controllerFactory = controllerFactory;
            this._databaseFileHandler = databaseFileHandler;
            this._activityContainerController = this.CreateChildController<ActivityContainerController>();
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
            }

            this.ViewModel.IsBusy = false;
        }
    }
}