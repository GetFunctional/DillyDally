using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TasksOverviewController : ControllerBase<TasksOverviewViewModel>
    {
        #region - Felder privat -

        private readonly IMediator _mediator;

        private TasksListController _openTasksListController;
        private TasksListController _repeatableTasksListController;
        private TasksListController _recentlyCompletedTasksListController;

        #endregion

        #region - Konstruktoren -

        public TasksOverviewController(TasksOverviewViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
        }

        #endregion

        #region - Methoden privat -

        protected override async Task OnInitializeAsync()
        {
            this._openTasksListController = await this._mediator.Send(new OpenTasksListControllerRequest()).ConfigureAwait(false);
            this.ViewModel.OpenTasksListViewModel = this._openTasksListController.ViewModel;

            this._repeatableTasksListController = await this._mediator.Send(new OpenTasksListControllerRequest()).ConfigureAwait(false);
            this.ViewModel.RepeatableTasksListViewModel = this._repeatableTasksListController.ViewModel;

            this._recentlyCompletedTasksListController = await this._mediator.Send(new OpenTasksListControllerRequest()).ConfigureAwait(false);
            this.ViewModel.RecentlyCompletedTasksListViewModel = this._recentlyCompletedTasksListController.ViewModel;
        }

        #endregion
    }
}