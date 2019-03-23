using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    public interface IViewFor<out TViewModel> where TViewModel : IViewModel
    {
        #region Properties, Indexers

        TViewModel ViewModel { get; }

        #endregion
    }
}