namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface INavigationAware
    {
        /// <summary>
        /// Wird synchron vom Navigator aufgerufen bevor die Navigation vom aktuellen Context weg geht.
        /// </summary>
        bool ConfirmNavigationAway();
    }
}