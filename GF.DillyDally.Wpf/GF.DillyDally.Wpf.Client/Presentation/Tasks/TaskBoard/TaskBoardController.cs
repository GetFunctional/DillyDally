using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;

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
                //var lanes = this._laneRepository.GetTaskBoardLanesAsync();
            }
        }
    }
}