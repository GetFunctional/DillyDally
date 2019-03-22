using GF.DillyDally.Data.Tasks;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TaskViewModelFactory
    {
        public TaskViewModel CreateFromTask(Task task)
        {
            return new TaskViewModel()
                   {
                       CompletedOn = task.CompletedOn,
                       Name = task.Name,
                       Workload = task.Workload,
                       CreatedOn = task.CreatedOn,
                       Description = task.Description,
                       DueDate = task.DueDate
                   };
        }
    }
}