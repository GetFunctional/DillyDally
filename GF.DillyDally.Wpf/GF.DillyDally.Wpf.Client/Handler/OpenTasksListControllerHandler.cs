using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler
{
    internal sealed class
        OpenTasksListControllerHandler : IRequestHandler<OpenTasksListControllerRequest, TasksListController>
    {
        #region Fields, Constants

        private readonly ControllerFactory<TasksListController> _tasksControllerFactory;
        private readonly ITasksRepository _tasksRepository;

        #endregion

        #region Constructors

        public OpenTasksListControllerHandler(ControllerFactory<TasksListController> tasksControllerFactory,
            ITasksRepository tasksRepository)
        {
            this._tasksControllerFactory = tasksControllerFactory;
            this._tasksRepository = tasksRepository;
        }

        #endregion

        #region Interface Implementations

        public async Task<TasksListController> Handle(OpenTasksListControllerRequest request,
            CancellationToken cancellationToken)
        {
            var controller = this._tasksControllerFactory.CreateController();
            controller.DataSource = () => this._tasksRepository.GetOpenTasksAsync();
            await controller.LoadDataAsync();
            return controller;
        }

        #endregion
    }
}