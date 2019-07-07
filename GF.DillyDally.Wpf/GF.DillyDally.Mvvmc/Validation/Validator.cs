using System;
using System.Collections.Generic;
using System.Linq;

namespace GF.DillyDally.Mvvmc.Validation
{
    public class Validator : IValidator
    {
        public Validator(IValidationRule validationRule) : this(new List<IValidationRule> {validationRule})
        {
        }

        public Validator(IEnumerable<IValidationRule> validationRules)
        {
            this.ValidationRules = new HashSet<IValidationRule>(validationRules);
        }

        public HashSet<IValidationRule> ValidationRules { get; }

        #region IValidator Members

        public string ValidateProperty(IValidateable validationObject, string propertyName)
        {
            var result = ValidationConstants.NoError();

            var validationRuleResults = this.ValidationRules
                .Select(rule => rule.ValidateProperty(validationObject, propertyName))
                .Where(err => !string.IsNullOrWhiteSpace(err)).ToArray();

            if (validationRuleResults.Any())
            {
                result += string.Join(Environment.NewLine, validationRuleResults);
            }

            return result;
        }

        public ValidationResult ValidateObject(IValidateable validationObject,
            ValidationCompleteness validationCompleteness)
        {
            var errors = new List<PropertyError>();

            errors.AddRange(this.ValidationRules.SelectMany(rule =>
                rule.ValidateObject(validationObject, validationCompleteness).Errors));

            var validationResult = new ValidationResult(errors);
            return validationResult;
        }

        public ValidationSummary ValidateObjects(IEnumerable<IValidateable> validationObjects,
            ValidationCompleteness validationCompleteness)
        {
            var errors = new List<KeyValuePair<IValidateable, ValidationResult>>();

            foreach (var validationObject in validationObjects)
            {
                errors.Add(new KeyValuePair<IValidateable, ValidationResult>(validationObject,
                    this.ValidateObject(validationObject, validationCompleteness)));
                if (validationCompleteness == ValidationCompleteness.ReturnOnFirstError && errors.Any())
                {
                    break;
                }
            }

            return new ValidationSummary(errors);
        }

        public void ClearValidationRules()
        {
            this.ValidationRules.Clear();
        }

        public void AddValidationRule(IValidationRule validationRule)
        {
            this.ValidationRules.Add(validationRule);
        }

        #endregion
    }
}