namespace GF.DillyDally.Wpf.Client.Core.Mediation.Navigation
{
    public sealed class NavigationResponse
    {
        public NavigationResponse(bool successful)
        {
            this.Successful = successful;
        }

        public bool Successful { get; set; }
    }
}