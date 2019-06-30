namespace GF.DillyDally.Mvvmc.Contracts
{
    public interface IController<TViewModel> : IController where TViewModel : IViewModel
    {
    }
}