using System.Collections.Generic;

namespace GF.DillyDally.Mvvmc.Validation
{
    public static class ValidationConstants
    {
        internal static readonly IEnumerable<string> PropertyNamesIgnoredDuringValidation = new List<string>
                                                                                            {
                                                                                                nameof(ViewModelBase.Error),
                                                                                                nameof(ViewModelBase.HasErrors),
                                                                                                nameof(ViewModelBase.IsBusy),
                                                                                            };

        public static string NoError()
        {
            return string.Empty;
        }
    }
}