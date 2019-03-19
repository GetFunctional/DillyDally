using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core
{
    public interface IViewFor<out TViewModel> where TViewModel : IViewModel
    {
        TViewModel ViewModel { get; }
    }
}