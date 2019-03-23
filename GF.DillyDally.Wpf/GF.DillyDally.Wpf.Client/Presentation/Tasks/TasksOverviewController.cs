using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public sealed class TasksOverviewController : ControllerBase<TasksOverviewViewModel>
    {
        private readonly IMediator _mediator;
        private TasksListController _openTasksListController;
        private TasksListController _recentlyCompletedTasksListController;
        private TasksListController _repeatableTasksListController;

        public TasksOverviewController(TasksOverviewViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
        }

        protected override async Task OnInitializeAsync()
        {
            await this.InitializeListControllersAsync();
        }

        private async Task InitializeListControllersAsync()
        {
            this._openTasksListController =
                await this._mediator.Send(new OpenTasksListControllerRequest()).ConfigureAwait(false);
            this.ViewModel.OpenTasksListViewModel = this._openTasksListController.ViewModel;

            this._repeatableTasksListController =
                await this._mediator.Send(new OpenTasksListControllerRequest()).ConfigureAwait(false);
            this.ViewModel.RepeatableTasksListViewModel = this._repeatableTasksListController.ViewModel;

            this._recentlyCompletedTasksListController =
                await this._mediator.Send(new OpenTasksListControllerRequest()).ConfigureAwait(false);
            this.ViewModel.RecentlyCompletedTasksListViewModel = this._recentlyCompletedTasksListController.ViewModel;
        }
    }
}