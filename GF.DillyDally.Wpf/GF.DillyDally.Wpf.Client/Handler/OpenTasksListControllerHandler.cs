using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler
{
    internal sealed class
        OpenTasksListControllerHandler : IRequestHandler<OpenTasksListControllerRequest, TasksListController>
    {
        private readonly ControllerFactory<TasksListController> _tasksControllerFactory;

        public OpenTasksListControllerHandler(ControllerFactory<TasksListController> tasksControllerFactory)
        {
            this._tasksControllerFactory = tasksControllerFactory;
        }

        #region IRequestHandler<OpenTasksListControllerRequest,TasksListController> Members

        public async Task<TasksListController> Handle(OpenTasksListControllerRequest request,
            CancellationToken cancellationToken)
        {
            var controller = await this._tasksControllerFactory.CreateControllerAsync();
            await controller.LoadDataAsync();
            return controller;
        }

        #endregion
    }
}