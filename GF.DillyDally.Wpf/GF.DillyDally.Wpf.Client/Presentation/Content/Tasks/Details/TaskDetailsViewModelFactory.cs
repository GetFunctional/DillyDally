using System.Linq;
using GF.DillyDally.ReadModel.Views.TaskDetails;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    internal sealed class TaskDetailsViewModelFactory
    {
        internal TaskSummaryViewModel CreateTaskSummaryViewModel(TaskDetailsEntity taskDetailData)
        {
            var taskSummaryViewModel = new TaskSummaryViewModel()
                                       {
                                           TaskName = taskDetailData.Name,
                                           DueDate = taskDetailData.DueDate,
                                           DefinitionOfDone = taskDetailData.DefinitionOfDone,
                                           Description = taskDetailData.Description,

                                       };

            
            if (taskDetailData.TaskImages.Any(x => x.IsPreviewImage))
            {
                taskSummaryViewModel.TaskPreviewImageBytes =
                    taskDetailData.TaskImages.First(x => x.IsPreviewImage).ImageBytesMedium;
            }

            return taskSummaryViewModel;
        }
    }
}