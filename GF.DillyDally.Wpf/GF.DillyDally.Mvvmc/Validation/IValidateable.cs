using System.ComponentModel;

namespace GF.DillyDally.Mvvmc.Validation
{
    public interface IValidateable : INotifyPropertyChanged, INotifyDataErrorInfo, IDataErrorInfo
    {
        #region Methoden (oeffentlich)

        string ClearError();

        void Validate();

        #endregion
    }
}