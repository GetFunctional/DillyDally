using System;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details.Fields
{
    internal class DateTaskActivityFieldViewModel : TaskActivityFieldViewModel
    {
        public DateTime? DateTimeValue { get; }

        public DateTaskActivityFieldViewModel(Guid activityFieldId, string fieldName, string unitOfMeasure, DateTime? dateTimeValue) : base(activityFieldId,fieldName,unitOfMeasure)
        {
            this.DateTimeValue = dateTimeValue;
        }
    }
}