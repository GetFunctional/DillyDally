using System.Linq;
using GF.DillyDally.Mvvmc.Validation;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    internal class ActivityFieldNamesValidationRule : ValidationRuleBase<ActivityFieldsPageViewModel>
    {
        public override string ValidateProperty(ActivityFieldsPageViewModel validationObject, string propertyName)
        {
            var fields = validationObject.ActivityFields;

            if (fields.Count > 1 && fields.GroupBy(x => x.FieldName).Count() > 1)
            {
                return "Fieldnames must be unique";
            }

            return this.NoError();
        }
    }
}