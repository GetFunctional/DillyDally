namespace GF.DillyDally.Mvvmc
{
    public abstract class ControllerBase<TViewModel> : InitializationBase, IController<TViewModel>
        where TViewModel : IViewModel
    {
        #region Constructors

        #region - Konstruktoren -

        protected ControllerBase(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        public TViewModel ViewModel { get; }

        #endregion

        #region IController<TViewModel> Members

        IViewModel IController.ViewModel
        {
            get { return this.ViewModel; }
        }

        #endregion

        #endregion
    }
}