using System;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details.Fields
{
    internal class IntegerTaskActivityFieldViewModel : TaskActivityFieldViewModel
    {
        public int? IntegerValue { get; }

        public IntegerTaskActivityFieldViewModel(Guid activityFieldId, string fieldName, string unitOfMeasure, int? integerValue) : base(activityFieldId,fieldName,unitOfMeasure)
        {
            this.IntegerValue = integerValue;
        }
    }
}