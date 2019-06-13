using System.Collections.Generic;

namespace GF.DillyDally.Mvvmc.Validation
{
    public interface IValidator
    {
        string ValidateProperty(IValidateable validationObject, string propertyName);

        ValidationResult ValidateObject(IValidateable validationObject,
            ValidationCompleteness validationCompleteness = ValidationCompleteness.ReturnOnFirstError);

        ValidationSummary ValidateObjects(IEnumerable<IValidateable> validationObjects,
            ValidationCompleteness validationCompleteness = ValidationCompleteness.ReturnOnFirstError);

        void ClearValidationRules();

        void AddValidationRule(IValidationRule validationRule);
    }
}