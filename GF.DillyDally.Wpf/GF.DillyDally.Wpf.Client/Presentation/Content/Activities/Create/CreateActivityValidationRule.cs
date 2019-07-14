using GF.DillyDally.Mvvmc.Validation;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public sealed class CreateActivityValidationRule : ValidationRuleBase<ActivityInfosPageViewModel>
    {
        public override string ValidateProperty(ActivityInfosPageViewModel validationObject, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(validationObject.ActivityName):
                    if (string.IsNullOrWhiteSpace(validationObject.ActivityName))
                    {
                        return "Please provide a name for the Activity";
                    }

                    break;

                case nameof(validationObject.SelectedActivityTypeViewModel):
                    if (validationObject.SelectedActivityTypeViewModel == null)
                    {
                        return "Please provide a category for the Activity";
                    }

                    break;
            }

            return this.NoError();
        }
    }
}