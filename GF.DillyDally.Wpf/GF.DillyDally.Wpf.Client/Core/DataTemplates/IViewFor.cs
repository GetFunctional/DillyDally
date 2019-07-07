using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    public interface IViewFor<out TViewModel> where TViewModel : IViewModel
    {
        TViewModel ViewModel { get; }
    }
}