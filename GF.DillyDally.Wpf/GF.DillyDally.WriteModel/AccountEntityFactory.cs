using System;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    internal sealed class AccountEntityFactory
    {
        internal AccountEntity CreateAccountForHolder(AccountHolderKey accountHolderKey, CurrencyEntity currency)
        {
            return this.CreateAccountForHolder(accountHolderKey, new CurrencyKey(currency.CurrencyId),
                $"Account for {currency.Name}");
        }

        internal AccountEntity CreateAccountForHolder(AccountHolderKey accountHolderKey, CurrencyKey currencyKey,
            string accountName)
        {
            return new AccountEntity(new AccountKey(Guid.NewGuid()))
            {
                Name = accountName,
                AccountHolderId = accountHolderKey.AccountHolderId,
                CurrencyId = currencyKey.CurrencyId
            };
        }
    }
}