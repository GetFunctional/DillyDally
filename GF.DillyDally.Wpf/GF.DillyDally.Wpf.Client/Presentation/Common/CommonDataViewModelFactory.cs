using GF.DillyDally.Data.Contracts.Entities;

namespace GF.DillyDally.Wpf.Client.Presentation.Common
{
    internal sealed class CommonDataViewModelFactory
    {
        internal CurrencyViewModel CreateCurrentViewModelFrom(ICurrencyEntity currencyEntity)
        {
            return new CurrencyViewModel(currencyEntity.CurrencyKey, currencyEntity.Name, currencyEntity.Code);
        }
    }
}