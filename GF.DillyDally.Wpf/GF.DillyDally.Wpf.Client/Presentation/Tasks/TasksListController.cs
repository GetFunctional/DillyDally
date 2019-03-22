using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TasksListController : ControllerBase<TasksListViewModel>
    {
        public TasksListController(TasksListViewModel viewModel) : base(viewModel)
        {
        }
    }
}