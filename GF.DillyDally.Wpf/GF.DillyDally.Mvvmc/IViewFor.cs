namespace GF.DillyDally.Mvvmc
{
    public interface IViewFor<out TViewModel> where TViewModel : IViewModel
    {
        TViewModel ViewModel { get; }
    }
}