using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.ContentNavigation
{
    public sealed class ContentNavigatorViewModel : ViewModelBase
    {
        #region - Felder privat -

        private IViewModel _displayTarget;

        #endregion

        #region - Methoden privat -

        internal void AssignDisplayTarget(IViewModel displayTarget)
        {
            this.DisplayTarget = displayTarget;
        }

        #endregion

        #region - Properties oeffentlich -

        public IViewModel DisplayTarget
        {
            get
            {
                return this._displayTarget;
            }
            set
            {
                this.SetField(ref this._displayTarget, value);
            }
        }

        #endregion
    }
}