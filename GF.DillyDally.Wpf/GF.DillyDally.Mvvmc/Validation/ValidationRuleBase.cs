using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GF.DillyDally.Mvvmc.Validation
{
    public abstract class ValidationRuleBase<TValidationObject> : IValidationRule<TValidationObject> where TValidationObject : class, IValidateable
    {
        #region IValidationRule<TValidationObject> Members

        public string ValidateProperty(IValidateable validationObject, string propertyName)
        {
            var match = validationObject as TValidationObject;
            return match == null ? String.Empty : this.ValidateProperty(match, propertyName);
        }

        public ValidationResult ValidateObject(IValidateable validationObject, ValidationCompleteness validationCompleteness)
        {
            var match = validationObject as TValidationObject;

            if (match == null) return ValidationResult.Empty();
            var result = this.ValidateObject(match, validationCompleteness);
            return result;
        }

        public ValidationSummary ValidateObjects(IEnumerable<IValidateable> validationObjects, ValidationCompleteness validationCompleteness)
        {
            return
                new ValidationSummary(
                    validationObjects.Select(v => new KeyValuePair<IValidateable, ValidationResult>(v, this.ValidateObject(v, validationCompleteness))));
        }

        public abstract string ValidateProperty(TValidationObject validationObject, string propertyName);

        public ValidationResult ValidateObject(TValidationObject validationObject, ValidationCompleteness validationCompleteness)
        {
            var infos = ValidateableReflector.GetValidateableProperties(validationObject);
            var errors = new List<PropertyError>();

            foreach (var info in infos)
            {
                if (validationCompleteness == ValidationCompleteness.ReturnOnFirstError && errors.Any())
                    break;

                var propertyName = info.Name;
                var error = this.ValidateProperty(validationObject, propertyName);
                if (!string.IsNullOrWhiteSpace(propertyName) && !string.IsNullOrWhiteSpace(error))
                {
                    errors.Add(new PropertyError(propertyName, error));
                }
            }

            return new ValidationResult(errors);
        }

        public ValidationSummary ValidateObjects(IEnumerable<TValidationObject> validationObjects, ValidationCompleteness validationCompleteness)
        {
            var errors = new List<KeyValuePair<IValidateable, ValidationResult>>();

            foreach (var validationObject in validationObjects)
            {
                errors.Add(new KeyValuePair<IValidateable, ValidationResult>(validationObject, this.ValidateObject(validationObject, validationCompleteness)));
                if (validationCompleteness == ValidationCompleteness.ReturnOnFirstError && errors.Any())
                {
                    break;
                }
            }

            return new ValidationSummary(errors);
        }

        protected string NoError()
        {
            return ValidationConstants.NoError();
        }

        #endregion

        private static class ValidateableReflector
        {
            #region - Felder privat -

            private static readonly Dictionary<Type, PropertyInfo[]> ItemTypeProperties = new Dictionary<Type, PropertyInfo[]>();

            #endregion

            #region - Methoden oeffentlich -

            internal static IEnumerable<PropertyInfo> GetValidateableProperties(object obj)
            {
                return GetValidateableProperties(obj.GetType());
            }

            private static IEnumerable<PropertyInfo> GetValidateableProperties(Type objectType)
            {
                if (ItemTypeProperties.ContainsKey(objectType))
                {
                    return ItemTypeProperties[objectType];
                }
                ItemTypeProperties.Add(objectType, GetPropertiesInternal(objectType).ToArray());
                return GetValidateableProperties(objectType);
            }

            #endregion

            #region - Methoden privat -

            private static IEnumerable<PropertyInfo> GetPropertiesInternal(Type objectType)
            {
                return objectType
                    .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(prop => !ValidationConstants.PropertyNamesIgnoredDuringValidation.Contains(prop.Name));
            }

            #endregion
        }
    }
}