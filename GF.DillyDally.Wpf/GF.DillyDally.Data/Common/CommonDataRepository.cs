using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Common
{
    internal sealed class CommonDataRepository : ICommonDataRepository
    {
        private readonly CurrencyEntityFactory _currencyEntityFactory = new CurrencyEntityFactory();

        #region ICommonDataRepository Members

        public Task<IList<CurrencyEntity>> GetAllCurrencies()
        {
            return Task.Run(() =>
            {
                IList<CurrencyEntity> listOfCurrencies = new List<CurrencyEntity>();

                var gameTime =
                    this._currencyEntityFactory.CreateCurrencyEntity(
                        new CurrencyKey(Guid.Parse("{7CB4BD57-8D91-4CA0-BA30-90D3600E0EC5}")), "Gametime", "min");
                var gameCredits =
                    this._currencyEntityFactory.CreateCurrencyEntity(
                        new CurrencyKey(Guid.Parse("{92375972-2B5A-464E-8E95-3F05E8FFAA04}")), "GameCredits", "€");
                var dayOff =
                    this._currencyEntityFactory.CreateCurrencyEntity(
                        new CurrencyKey(Guid.Parse("{E3B392F8-369F-41C6-BFAB-60B9B40446D0}")), "Days off", "tage");
                var hsMatches =
                    this._currencyEntityFactory.CreateCurrencyEntity(
                        new CurrencyKey(Guid.Parse("{D1C375E9-8C11-4AC4-95FC-92FD2BF7D736}")), "Hearthstone Matches",
                        "games");
                var calories =
                    this._currencyEntityFactory.CreateCurrencyEntity(
                        new CurrencyKey(Guid.Parse("{F7FDD4C4-2636-4A9A-B633-B0C4FE710995}")), "Extra Kalorien",
                        "kcal");
                var gold =
                    this._currencyEntityFactory.CreateCurrencyEntity(
                        new CurrencyKey(Guid.Parse("{C3152247-153D-4230-8CFB-8C8FD25033B3}")), "Gold", "g");

                listOfCurrencies.Add(gameTime);
                listOfCurrencies.Add(gameCredits);
                listOfCurrencies.Add(dayOff);
                listOfCurrencies.Add(hsMatches);
                listOfCurrencies.Add(calories);
                listOfCurrencies.Add(gold);

                return listOfCurrencies;
            });
        }

        #endregion
    }
}