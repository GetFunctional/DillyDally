using GF.DillyDally.Data.Contracts.Entities;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TaskViewModelFactory
    {
        public TaskViewModel CreateFromTask(ITaskEntity taskEntity)
        {
            return new TaskViewModel(taskEntity.TaskKey)
            {
                Name = taskEntity.Name,
                CreatedOn = taskEntity.CreatedOn,
                Description = taskEntity.Description,
                DueDate = taskEntity.DueDate
            };
        }

        public TaskViewModel CreateFromTask(IOpenTaskEntity taskEntity)
        {
            return new TaskViewModel(taskEntity.TaskKey)
            {
                Name = taskEntity.Name,
                Description = taskEntity.Description,
                DueDate = taskEntity.DueDate
            };
        }
    }
}