using System;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details.Fields
{
    internal class DecimalTaskActivityFieldViewModel : TaskActivityFieldViewModel
    {
        public decimal? DecimalValue { get; }

        public DecimalTaskActivityFieldViewModel(Guid activityFieldId, string fieldName, string unitOfMeasure, decimal? decimalValue) : base(activityFieldId,fieldName,unitOfMeasure)
        {
            this.DecimalValue = decimalValue;
        }
    }
}