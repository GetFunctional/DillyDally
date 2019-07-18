using GF.DillyDally.Mvvmc.Validation;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    public class ActivityFieldItemValidationRule : ValidationRuleBase<ActivityFieldItemViewModel>
    {
        public override string ValidateProperty(ActivityFieldItemViewModel validationObject, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(ActivityFieldItemViewModel.FieldName):
                    if (string.IsNullOrEmpty(validationObject.FieldName))
                    {
                        return "Please provide a name for the field.";
                    }

                    if (validationObject.FieldName.Length > 255)
                    {
                        return "The Name must not exceed 255 characters.";
                    }
                    break;

                case nameof(ActivityFieldItemViewModel.FieldType):
                    if (validationObject.FieldType == null )
                    {
                        return "Please provide a type for the field.";
                    }
                    break;
            }

            return ValidationConstants.NoError();
        }
    }
}