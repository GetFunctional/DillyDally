using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GF.DillyDally.Mvvmc.Validation;
using ReactiveUI;

namespace GF.DillyDally.Mvvmc
{
    public abstract class ViewModelBase : ObservableObject, IViewModel, IValidateable
    {
        private string _error;
        private bool _hasErrors;
        private bool _isBusy;

        protected ViewModelBase() : this(new Validator(Enumerable.Empty<IValidationRule>()))
        {
        }

        protected ViewModelBase(IValidator validator)
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
                return this.Validator == null
                    ? ValidationConstants.NoError()
                    : this.AssignError(this.Validator.ValidateProperty(this, propertyName));
            }
        }

        public string ClearError()
        {
            return this.AssignError(ValidationConstants.NoError());
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // return all errors
                return this.Validator.ValidateObject(this, ValidationCompleteness.CompleteValidation).Errors.Select(x => x.Error);
            }

            // return the error for the parameter 'propertyName'
            return this[propertyName];
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

        public void Validate()
        {
            this.ClearError();
            var validationResult = this.Validator.ValidateObject(this, ValidationCompleteness.CompleteValidation);
            if (validationResult.ValidationState != ValidationState.Valid)
            {
                this.AssignError(validationResult.Errors);
                foreach (var validationResultError in validationResult.Errors)
                {
                    this.RaiseErrorsChanged(new DataErrorsChangedEventArgs(validationResultError.PropertyName));
                }
            }
        }

        #endregion

        #region IViewModel Members

        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._isBusy, value);
            }
        }

        #endregion

        private string AssignError(string error)
        {
            this.Error = error;
            this.HasErrors = !string.IsNullOrEmpty(this.Error);
            return this.Error;
        }

        private void AssignError(IReadOnlyList<PropertyError> errors)
        {
            this.AssignError(string.Join(Environment.NewLine, errors.Select(x => x.Error)));
        }

        protected virtual void RaiseErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }
    }
}