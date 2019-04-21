﻿using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies.CreateCurrency
{
    public sealed class NewCurrencyResponse
    {
        public NewCurrencyResponse(CurrencyKey newCurrencyKey)
        {
            this.NewNewCurrencyKey = newCurrencyKey;
            this.DialogConfirmed = true;
        }

        public NewCurrencyResponse()
        {
            this.DialogConfirmed = false;
        }

        public CurrencyKey NewNewCurrencyKey { get; }

        public bool DialogConfirmed { get; }
    }
}