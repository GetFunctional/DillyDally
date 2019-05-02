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

        #region IController<TViewModel> Members

        IViewModel IController.ViewModel
        {
            get
            {
                return this.ViewModel;
            }
        }

        #endregion
    }
}