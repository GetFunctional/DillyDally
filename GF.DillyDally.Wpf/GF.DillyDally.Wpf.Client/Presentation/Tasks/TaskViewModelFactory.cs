using GF.DillyDally.Data.Tasks;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TaskViewModelFactory
    {
        public TaskViewModel CreateFromTask(TaskEntity taskEntity)
        {
            return new TaskViewModel
            {
                CompletedOn = taskEntity.CompletedOn,
                Name = taskEntity.Name,
                Workload = taskEntity.Workload,
                CreatedOn = taskEntity.CreatedOn,
                Description = taskEntity.Description,
                DueDate = taskEntity.DueDate
            };
        }
    }
}