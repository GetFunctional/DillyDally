using System.Collections.Generic;
using System.Linq;

namespace GF.DillyDally.Mvvmc.Validation
{
    public sealed class ValidationResult
    {
        private ValidationResult() : this(new List<PropertyError>())
        {
        }

        public ValidationResult(IReadOnlyList<PropertyError> errors)
        {
            this.ValidationState = errors.Any() ? ValidationState.Invalid : ValidationState.Valid;
            this.Errors = errors;
        }

        public ValidationState ValidationState { get; }
        public IReadOnlyList<PropertyError> Errors { get; }

        public static ValidationResult Empty()
        {
            return new ValidationResult();
        }
    }
}