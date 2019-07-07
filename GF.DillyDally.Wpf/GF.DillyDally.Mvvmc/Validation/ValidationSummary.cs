using System.Collections.Generic;
using System.Linq;

namespace GF.DillyDally.Mvvmc.Validation
{
    public class ValidationSummary
    {
        public ValidationSummary(IEnumerable<KeyValuePair<IValidateable, ValidationResult>> validationResults)
        {
            var keyValuePairs = validationResults as IList<KeyValuePair<IValidateable, ValidationResult>> ??
                                validationResults.ToList();
            this.ValidResults = keyValuePairs.Where(m => m.Value.ValidationState == ValidationState.Valid).ToList();
            this.InvalidResults = keyValuePairs.Where(m => m.Value.ValidationState == ValidationState.Invalid).ToList();
            this.UndeterminedResults = keyValuePairs.Where(m => m.Value.ValidationState == ValidationState.Undetermined)
                .ToList();
        }

        public ValidationState ValidationState
        {
            get
            {
                return this.InvalidResults.Any()
                    ? ValidationState.Invalid
                    : this.UndeterminedResults.Any()
                        ? ValidationState.Undetermined
                        : ValidationState.Valid;
            }
        }

        public List<KeyValuePair<IValidateable, ValidationResult>> ValidResults { get; }

        public List<KeyValuePair<IValidateable, ValidationResult>> UndeterminedResults { get; }

        public List<KeyValuePair<IValidateable, ValidationResult>> InvalidResults { get; }
    }
}