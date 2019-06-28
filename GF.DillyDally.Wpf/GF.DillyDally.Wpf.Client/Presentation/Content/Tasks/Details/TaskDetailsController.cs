using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Views.TaskBoard;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskDetailsController : ControllerBase<TaskDetailsViewModel>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public TaskDetailsController(TaskDetailsViewModel viewModel, DatabaseFileHandler databaseFileHandler) : base(viewModel)
        {
            this._databaseFileHandler = databaseFileHandler;
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
            }

            this.ViewModel.IsBusy = true;
        }
    }
}