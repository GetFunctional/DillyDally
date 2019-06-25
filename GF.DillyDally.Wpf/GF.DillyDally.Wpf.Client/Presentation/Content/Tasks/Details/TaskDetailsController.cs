using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskDetailsController : ControllerBase<TaskDetailsViewModel>
    {
        public TaskDetailsController(TaskDetailsViewModel viewModel) : base(viewModel)
        {
        }

        public async Task LoadTaskDetailsAsync(Guid taskId)
        {
            await Task.CompletedTask;
        }
    }
}