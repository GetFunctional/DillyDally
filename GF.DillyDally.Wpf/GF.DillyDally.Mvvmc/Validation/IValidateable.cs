using System.ComponentModel;

namespace GF.DillyDally.Mvvmc.Validation
{
    public interface IValidateable : INotifyPropertyChanged, INotifyDataErrorInfo, IDataErrorInfo
    {
        bool Validate(IValidator validator);

        bool Validate();
    }
}