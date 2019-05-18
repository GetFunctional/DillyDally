using System.ComponentModel;

namespace GF.DillyDally.Mvvmc
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; }
    }
}