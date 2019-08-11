using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details.Fields;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    internal sealed class TaskDetailsViewModelFactory
    {
        //internal TaskSummaryViewModel CreateTaskSummaryViewModel(TaskDetailsEntity taskDetailData)
        //{
        //    var taskSummaryViewModel = new TaskSummaryViewModel
        //                               {
        //                                   TaskName = taskDetailData.Name,
        //                                   DueDate = taskDetailData.DueDate,
        //                                   DefinitionOfDone = taskDetailData.DefinitionOfDone,
        //                                   Description = taskDetailData.Description
        //                               };


        //    if (taskDetailData.TaskImages.Any(x => x.IsPreviewImage))
        //    {
        //        taskSummaryViewModel.TaskPreviewImageBytes =
        //            taskDetailData.TaskImages.First(x => x.IsPreviewImage).ImageBytesMedium;
        //    }

        //    return taskSummaryViewModel;
        //}

        //internal TaskActivitiesViewModel CreateTaskActivitiesViewModel(TaskDetailsEntity taskDetailData)
        //{
        //    var taskActivityItems = this.CreateTaskActivityItemViewModels(taskDetailData.TaskActivities, taskDetailData.TaskActivityFields).ToList();
        //    return new TaskActivitiesViewModel(taskActivityItems);
        //}

        //private IEnumerable<TaskActivityItemViewModel> CreateTaskActivityItemViewModels(IReadOnlyList<TaskDetailsActivityEntity> taskActivities,
        //    IReadOnlyList<TaskActivityFieldEntity> taskActivityFields)
        //{
        //    foreach (var taskDetailsActivityEntity in taskActivities)
        //    {
        //        yield return this.CreateTaskActivityItemViewModel(taskDetailsActivityEntity,
        //            taskActivityFields.Where(x => x.ActivityId == taskDetailsActivityEntity.ActivityId));
        //    }
        //}

        //private TaskActivityItemViewModel CreateTaskActivityItemViewModel(TaskDetailsActivityEntity taskDetailsActivityEntity,
        //    IEnumerable<TaskActivityFieldEntity> activityFields)
        //{
        //    return new TaskActivityItemViewModel(this.CreateTaskActivityFields(activityFields));
        //}

        //private IEnumerable<TaskActivityFieldViewModel> CreateTaskActivityFields(IEnumerable<TaskActivityFieldEntity> activityFields)
        //{
        //    return activityFields.Select(this.CreateTaskActivityField);
        //}

        //private TaskActivityFieldViewModel CreateTaskActivityField(TaskActivityFieldEntity af)
        //{
        //    switch (af.FieldType)
        //    {
        //        case ActivityFieldType.Integer:
        //            return new IntegerTaskActivityFieldViewModel(af.ActivityFieldId, af.Name, af.UnitOfMeasure, af.IntegerValue);
        //        case ActivityFieldType.Decimal:
        //            return new DecimalTaskActivityFieldViewModel(af.ActivityFieldId, af.Name, af.UnitOfMeasure, af.DecimalValue);
        //        case ActivityFieldType.Text:
        //            return new TextTaskActivityFieldViewModel(af.ActivityFieldId, af.Name, af.UnitOfMeasure, af.StringValue);
        //        case ActivityFieldType.Date:
        //            return new DateTaskActivityFieldViewModel(af.ActivityFieldId, af.Name, af.UnitOfMeasure, af.DateTimeValue);
        //        case ActivityFieldType.DateTime:
        //            return new DateTimeTaskActivityFieldViewModel(af.ActivityFieldId, af.Name, af.UnitOfMeasure, af.DateTimeValue);
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}
    }
}