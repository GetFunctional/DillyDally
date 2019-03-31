using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TasksListController : ControllerBase<TasksListViewModel>
    {
        private readonly IMediator _mediator;
        private readonly TaskViewModelFactory _taskViewModelFactory = new TaskViewModelFactory();

        public TasksListController(TasksListViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
            this.ViewModel.AddTaskCommand = new AddTaskCommand(async name => await this.AddTask(name));
        }

        public Func<Task<IList<IOpenTaskEntity>>> ExternalDataSource { get; set; }

        private async Task AddTask(string initialName)
        {
            // Dialog aufrufen für Details
            var response = await this._mediator.Send(new CreateNewTaskRequest(initialName, TaskType.SingleCompletion));
            if (!response.ProcessCanceled)
            {
                // Get new Task for List
                await this.LoadDataAsync();
            }
        }

        public async Task<IList<TaskViewModel>> LoadDataAsync()
        {
            var data = await this.ExternalDataSource();
            this.ViewModel.Tasks =
                new ObservableCollection<TaskViewModel>(data.Select(t => this._taskViewModelFactory.CreateFromTask(t)));
            return this.ViewModel.Tasks;
        }
    }
}