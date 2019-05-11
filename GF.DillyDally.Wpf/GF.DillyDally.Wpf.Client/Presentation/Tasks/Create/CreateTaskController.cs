using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks.Create
{
    public class CreateTaskController : DialogControllerBase<CreateTaskViewModel>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public CreateTaskController(CreateTaskViewModel viewModel, DatabaseFileHandler databaseFileHandler) :
            base(viewModel)
        {
            this._databaseFileHandler = databaseFileHandler;
            viewModel.CreateTaskCommand =
                ReactiveCommand.Create(() => this.ConfirmDialogWith(this.CreateTaskDialogResult));
            viewModel.CancelProcessCommand =
                ReactiveCommand.Create(() => this.ConfirmDialogWith(this.CancelDialogResult));

            viewModel.TaskAchievementsViewModel = new TaskAchievementsViewModel();
        }

        public IDialogResult CreateTaskDialogResult { get; } = new DialogCommandResult();
        public IDialogResult CancelDialogResult { get; } = new DialogCommandResult();


        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            //using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            //{
            //    var taskBoardRepository = new TaskBoardRepository();
            //    var lanes = await taskBoardRepository.GetTaskBoardLanesAsync(connection);

            //    var laneViewModels = this.CreateLaneViewModels(lanes);
            //    this.ViewModel.Lanes = laneViewModels;
            //}
        }

        //private IList<TaskBoardLaneViewModel> CreateLaneViewModels(IList<TaskBoardLaneEntity> lanes)
        //{
        //    return lanes.Select(lane =>
        //    {
        //        var laneVm = new TaskBoardLaneViewModel();
        //        laneVm.LaneName = lane.Name;

        //        laneVm.Tasks = new ObservableCollection<TaskBoardTaskViewModel>(lane.Tasks.Select(task =>
        //       {
        //           var taskVm = new TaskBoardTaskViewModel(task.Name, task.DisplayLink, task.Category.ColorCode, task.Category.Name, 3);
        //           taskVm.Name = task.Name;
        //           return taskVm;
        //       }));

        //        return laneVm;
        //    }).ToList();
        //}
    }
}