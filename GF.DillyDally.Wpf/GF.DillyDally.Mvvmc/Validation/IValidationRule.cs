using System.Collections.Generic;

namespace GF.DillyDally.Mvvmc.Validation
{
    public interface IValidationRule
    {
        #region - Methoden oeffentlich -

        string ValidateProperty(IValidateable validationObject, string propertyName);

        ValidationResult ValidateObject(IValidateable validationObject, ValidationCompleteness validationCompleteness);

        ValidationSummary ValidateObjects(IEnumerable<IValidateable> validationObjects, ValidationCompleteness validationCompleteness);

        #endregion
    }
}