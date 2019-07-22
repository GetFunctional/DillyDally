using System;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details.Fields
{
    internal class TextTaskActivityFieldViewModel : TaskActivityFieldViewModel
    {
        public string TextValue { get; }

        public TextTaskActivityFieldViewModel(Guid activityFieldId, string fieldName, string unitOfMeasure, string textValue) : base(activityFieldId,fieldName,unitOfMeasure)
        {
            this.TextValue = textValue;
        }
    }
}