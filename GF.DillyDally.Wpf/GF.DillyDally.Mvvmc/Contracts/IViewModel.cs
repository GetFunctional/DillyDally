using System.ComponentModel;

namespace GF.DillyDally.Mvvmc.Contracts
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; }
    }
}