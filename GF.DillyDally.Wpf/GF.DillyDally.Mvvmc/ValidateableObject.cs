using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GF.DillyDally.Mvvmc.Validation;
using ReactiveUI;

namespace GF.DillyDally.Mvvmc
{
    public abstract class ValidateableObject : ObservableObject, IValidateable
    {
        private string _error;
        private bool _hasErrors;
        private bool _isValidationPending;

        protected ValidateableObject() : this(new Validator(Enumerable.Empty<IValidationRule>()))
        {
        }

        protected ValidateableObject(IValidator validator)
        {
            this.Validator = validator;
        }

        [Browsable(false)]
        protected IValidator Validator { get; }

        #region IValidateable Members

        [Browsable(false)]
        public string Error
        {
            get
            {
                return this._error;
            }
            private set
            {
                this.RaiseAndSetIfChanged(ref this._error, value);
            }
        }

        /// <summary>
        ///     Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        [Browsable(false)]
        public virtual string this[string propertyName]
        {
            get
            {
                if (this._isValidationPending)
                {
                    var errorToReturn = this.Validator == null
                        ? ValidationConstants.NoError()
                        : this.AssignError(new[] {this.Validator.ValidateProperty(this, propertyName)});

                    return errorToReturn;
                }

                return ValidationConstants.NoError();
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // Do not return errors for unknown Bindings
                return Enumerable.Empty<string>();
            }

            // return the error for the parameter 'propertyName'
            return new List<string> {this[propertyName]};
        }

        public bool HasErrors
        {
            get
            {
                return this._hasErrors;
            }
            private set
            {
                this.RaiseAndSetIfChanged(ref this._hasErrors, value);
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool Validate()
        {
            return this.Validate(this.Validator);
        }

        public bool Validate(IValidator validator)
        {
            this.ClearError();
            this.StartValidation();
            var validationHook = this.BeforeValidate();
            var validationResult = validator.ValidateObject(this, ValidationCompleteness.CompleteValidation);
            if (validationResult.ValidationState != ValidationState.Valid)
            {
                this.AssignError(validationResult.Errors);

                foreach (var validationResultError in validationResult.Errors)
                {
                    this.RaiseErrorsChanged(new DataErrorsChangedEventArgs(validationResultError.PropertyName));
                }

                // Führt dazu das bestehende invalide Markierungen wieder aufgehoben werden können ( z.B. durch ValidatesOnDataErrors )
                this.HasErrors = false;
                return false;
            }

            this.HasErrors = false;
            return validationHook;
        }

        #endregion

        protected void SuspendValidation()
        {
            this._isValidationPending = false;
        }

        protected void StartValidation()
        {
            this._isValidationPending = true;
        }

        public string ClearError()
        {
            this.SuspendValidation();
            return this.AssignError(new[] {ValidationConstants.NoError()});
        }

        private string AssignError(IEnumerable<string> errors)
        {
            this.Error = string.Join(Environment.NewLine, errors.Where(err => !string.IsNullOrWhiteSpace(err)));
            return this.Error;
        }

        private void AssignError(IReadOnlyList<PropertyError> errors)
        {
            this.AssignError(errors.Select(pe => pe.Error));
        }

        protected virtual void RaiseErrorsChanged(DataErrorsChangedEventArgs e)
        {
            // HasErrors muss befüllt sein bevor ErrorsChanged ausgelöst wird.
            this.HasErrors = !string.IsNullOrEmpty(this.Error);
            ErrorsChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Override this method to trigger validation of child viewmodels.
        /// </summary>
        /// <returns>Result of the validation</returns>
        protected virtual bool BeforeValidate()
        {
            return true;
        }
    }
}