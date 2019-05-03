using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Views.TaskBoard;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TaskBoardController : ControllerBase<TaskBoardViewModel>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public TaskBoardController(TaskBoardViewModel viewModel, DatabaseFileHandler databaseFileHandler) : base(viewModel)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                var taskBoardRepository = new TaskBoardRepository();
                var lanes = await taskBoardRepository.GetTaskBoardLanesAsync(connection);

                var laneViewModels = this.CreateLaneViewModels(lanes);
                this.ViewModel.Lanes = laneViewModels;
            }
        }

        private IList<TaskBoardLaneViewModel> CreateLaneViewModels(IList<TaskBoardLaneEntity> lanes)
        {
            return new List<TaskBoardLaneViewModel>();
        }
    }
}