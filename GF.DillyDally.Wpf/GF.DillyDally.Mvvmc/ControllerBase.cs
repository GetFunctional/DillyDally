namespace GF.DillyDally.Mvvmc
{
    public abstract class ControllerBase<TViewModel> : InitializationBase, IController<TViewModel>
        where TViewModel : IViewModel
    {
        protected ControllerBase(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; }
    }
}