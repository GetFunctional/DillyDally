namespace GF.DillyDally.Mvvmc
{
    public interface IController<TViewModel> : IController where TViewModel : IViewModel
    {
    }
}