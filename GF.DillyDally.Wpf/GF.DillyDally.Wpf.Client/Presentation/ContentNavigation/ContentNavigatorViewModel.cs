using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentNavigatorViewModel : ViewModelBase
    {
        #region - Felder privat -

        private IViewModel _displayTarget;
        private string _contentTitle;

        #endregion

        #region - Methoden privat -

        internal void AssignDisplayTarget(IViewModel displayTarget, string contentTitle)
        {
            this.DisplayTarget = displayTarget;
            this.ContentTitle = contentTitle;
        }

        #endregion

        #region - Properties oeffentlich -

        public IViewModel DisplayTarget
        {
            get
            {
                return this._displayTarget;
            }
            private set
            {
                this.SetField(ref this._displayTarget, value);
            }
        }

        public string ContentTitle
        {
            get
            {
                return this._contentTitle;
            }
            private set
            {
                this.SetField(ref this._contentTitle, value);
            }
        }

        #endregion
    }
}