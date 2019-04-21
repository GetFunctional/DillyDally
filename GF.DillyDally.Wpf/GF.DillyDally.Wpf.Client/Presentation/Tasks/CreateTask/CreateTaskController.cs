using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.WriteModel;
using GF.DillyDally.WriteModel.Deprecated;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks.CreateTask
{
    public class CreateTaskController : ControllerBase<CreateTaskViewModel>
    {
        private readonly ITaskService _taskService;
        private ITask _newTask;

        public CreateTaskController(ITaskService taskService, CreateTaskViewModel viewModel) : base(viewModel)
        {
            this._taskService = taskService;
        }

        public void CreateNewTask(string initialName, TaskType requestTaskType)
        {
            this._newTask = this._taskService.CreateNewTask(initialName,requestTaskType);
            var newTask = this._newTask;
            this.ViewModel.TaskKey = newTask.TaskKey;
            this.ViewModel.TaskName = newTask.Name;
            this.ViewModel.TaskType = newTask.TaskType;
        }

        public bool ValidateTaskData()
        {
            return !string.IsNullOrWhiteSpace(this.ViewModel.TaskName);
        }

        public async Task<TaskKey> SaveNewTask()
        {
            var taskData = this.CollectDataFromViewModel();
            var newTaskKey = await this._taskService.SaveTaskAsync(taskData);
            return newTaskKey;
        }

        private ITask CollectDataFromViewModel()
        {
            this._newTask.Name = this.ViewModel.TaskName;
            this._newTask.Description = this.ViewModel.Description;
            this._newTask.DueDate = this.ViewModel.DueDate;
            if (this.ViewModel.TaskType != this._newTask.TaskType)
            {
                this._newTask.ChangeTaskType(this.ViewModel.TaskType);
            }

            return this._newTask;
        }
    }
}