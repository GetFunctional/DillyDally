using GF.DillyDally.ReadModel.Common;

namespace GF.DillyDally.Wpf.Client.Presentation.Common
{
    internal class CommonDataViewModelFactory
    {
        internal CurrencyViewModel CreateCurrentViewModelFrom(CurrencyEntity currencyEntity)
        {
            return new CurrencyViewModel(currencyEntity.CurrencyKey, currencyEntity.Name, currencyEntity.Code);
        }
    }
}