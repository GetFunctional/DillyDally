using System.Collections.Generic;
using System.Data;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Sqlite.Entities;
using Z.Dapper.Plus;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class FoundationDataProvider
    {
        private readonly EntityFactory _entityFactory = new EntityFactory();


        public void InsertBaseDataIntoDatabase(IDbConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                // Insert Currencies
                var baseCurrencies = this.CreateBaseCurrencies();
                connection.BulkInsert(baseCurrencies);

                // Basierend auf den Currencies die Konten erstellen
                var accountBalances = this.CreateAccountsBalances(baseCurrencies);
                connection.BulkInsert(accountBalances);

                // RewardTemplates anlegen
                var rewardTemplates = this.CreateRewardTemplates(baseCurrencies);
                connection.BulkInsert(rewardTemplates);

                transaction.Commit();
            }
        }

        private List<RewardTemplateEntity> CreateRewardTemplates(List<CurrencyEntity> baseCurrencies)
        {
            var baseRewards = new List<RewardTemplateEntity>();

            decimal gametimeInitialValueBegin = 8;
            decimal gametimeInitialValueEnd = 12;
            decimal goldInitialValueBegin = 17;
            decimal goldInitialValueEnd = 23;
            var hsInitialValueBegin = 0.75m;
            var hsInitialValueEnd = 1.25m;
            var credInitialValueBegin = 0.25m;
            var credInitialValueEnd = 0.5m;
            var rareMultiplier = 2m;
            var epicMultiplier = 2m * 2.5m;
            var legendaryMultiplier = 2m * 2.5m * 3m;

            foreach (var currencyEntity in baseCurrencies)
            {
                var currentCurrencyKey = currencyEntity.CurrencyKey;
                switch (currencyEntity.Name)
                {
                    case "Gametime":
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Common,
                            "Some Gametime", gametimeInitialValueBegin, gametimeInitialValueEnd));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Rare,
                            "More Gametime", gametimeInitialValueBegin * rareMultiplier,
                            gametimeInitialValueEnd * rareMultiplier));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Epic,
                            "Much Gametime", gametimeInitialValueBegin * epicMultiplier,
                            gametimeInitialValueEnd * epicMultiplier));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Legendary,
                            "Very Much Gametime", gametimeInitialValueBegin * legendaryMultiplier,
                            gametimeInitialValueEnd * legendaryMultiplier));
                        break;

                    case "Gold":
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Common,
                            "Some Gold", goldInitialValueBegin, goldInitialValueEnd, false, true));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Rare,
                            "More Gold", goldInitialValueBegin * rareMultiplier, goldInitialValueEnd * rareMultiplier,
                            false, true));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Epic,
                            "Much Gold", goldInitialValueBegin * epicMultiplier, goldInitialValueEnd * epicMultiplier,
                            false, true));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Legendary,
                            "Very Much Gold", goldInitialValueBegin * legendaryMultiplier,
                            goldInitialValueEnd * legendaryMultiplier, false, true));
                        break;

                    case "HS Matches":
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Common,
                            "Some HS Matches", hsInitialValueBegin, hsInitialValueEnd));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Rare,
                            "More HS Matches", hsInitialValueBegin * rareMultiplier,
                            hsInitialValueEnd * rareMultiplier));
                        break;

                    case "Gamecredits":
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Common,
                            "Some Gamecredits", credInitialValueBegin, credInitialValueEnd));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Rare,
                            "More Gamecredits", credInitialValueBegin * rareMultiplier,
                            credInitialValueEnd * rareMultiplier));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Epic,
                            "Much Gamecredits", credInitialValueBegin * epicMultiplier,
                            credInitialValueEnd * epicMultiplier));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Legendary,
                            "Very Much Gamecredits", credInitialValueBegin * legendaryMultiplier,
                            credInitialValueEnd * legendaryMultiplier));
                        break;

                    case "Days off":
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.Legendary,
                            "Take a day off", 0.25m, 0.75m));
                        break;

                    case "Randomdrops":
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.None,
                            "1 Randomdrop", 1, 1, true, true));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.None,
                            "2 Randomdrops", 2, 2, true, true));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.None,
                            "4 Randomdrops", 4, 4, true, true));
                        baseRewards.Add(this._entityFactory.CreateRewardTemplate(currentCurrencyKey, Rarity.None,
                            "8 Randomdrops", 8, 8, true, true));
                        break;
                }
            }

            return baseRewards;
        }

        private List<AccountBalanceEntity> CreateAccountsBalances(List<CurrencyEntity> baseCurrencies)
        {
            var accountBalances = new List<AccountBalanceEntity>();
            foreach (var currencyEntity in baseCurrencies)
            {
                var balanceForCurrency =
                    this._entityFactory.CreateAccountBalanceEntity(currencyEntity.CurrencyKey, currencyEntity.Name);
                accountBalances.Add(balanceForCurrency);
            }

            return accountBalances;
        }

        private List<CurrencyEntity> CreateBaseCurrencies()
        {
            var gameTimeCurrency = this._entityFactory.CreateCurrencyEntity("Gametime", "min");
            var goldCurrency = this._entityFactory.CreateCurrencyEntity("Gold", "g");
            var hearthstoneMatchesCurrency = this._entityFactory.CreateCurrencyEntity("HS Matches", "games");
            var gameCreditsCurrency = this._entityFactory.CreateCurrencyEntity("Gamecredits", "€");
            var daysOffCurrency = this._entityFactory.CreateCurrencyEntity("Days off", "Tage");
            var randoms = this._entityFactory.CreateCurrencyEntity("Randomdrops", "drops");
            var baseCurrencies = new List<CurrencyEntity>
            {
                gameCreditsCurrency, goldCurrency, gameTimeCurrency, hearthstoneMatchesCurrency, daysOffCurrency,
                randoms
            };
            return baseCurrencies;
        }
    }
}