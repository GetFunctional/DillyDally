namespace GF.DillyDally.Wpf.Client.Core.Mediation.Navigation
{
    internal sealed class NavigationResponse
    {
        #region Constructors

        public NavigationResponse(bool successful)
        {
            this.Successful = successful;
        }

        #endregion

        #region Properties, Indexers

        public bool Successful { get; set; }

        #endregion
    }
}