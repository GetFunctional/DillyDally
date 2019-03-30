using System;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Data.Sqlite.Entities;

namespace GF.DillyDally.Data.Sqlite
{
    public sealed class EntityFactory
    {
        private readonly IGuidGenerator _guidGenerator;

        public EntityFactory()
        {
            this._guidGenerator = new GuidGenerator();
        }

        public AccountBalanceEntity CreateAccountBalanceEntity(CurrencyKey currencyKey, string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName) || currencyKey.CurrencyId == Guid.Empty)
            {
                throw new ArgumentException();
            }

            return new AccountBalanceEntity
            {
                CurrencyKey = currencyKey,
                AccountName = accountName,
                AccountBalanceId = this._guidGenerator.GenerateGuid()
            };
        }


        public AccountBalanceTransactionEntity CreateAccountBalanceTransactionEntity(CurrencyKey currencyKey,
            decimal amount, AccountBalanceKey accountKey)
        {
            if (currencyKey.CurrencyId == Guid.Empty || accountKey.AccountBalanceId == Guid.Empty)
            {
                throw new ArgumentException();
            }

            return new AccountBalanceTransactionEntity
            {
                AccountBalanceTransactionId = this._guidGenerator.GenerateGuid(),
                CurrencyKey = currencyKey,
                Amount = amount,
                AccountBalanceKey = accountKey
            };
        }

        public CurrencyEntity CreateCurrencyEntity(string name, string code)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }

            return new CurrencyEntity
            {
                CurrencyId = this._guidGenerator.GenerateGuid(),
                Name = name,
                Code = code
            };
        }

        //public IRewardEntity CreateRewardEntity()
        //{

        //}

        //public IRewardTemplateEntity CreateRewardTemplateEntity()
        //{

        //}

        //public ITaskCompletionEntity CreateTaskCompletionEntity()
        //{

        //}

        //public ITaskEntity CreateTaskEntity()
        //{

        //}

        //public ITaskRewardEntity CreateTaskRewardEntity()
        //{

        //}
        public RewardTemplateEntity CreateRewardTemplate(CurrencyKey currencyKey, Rarity rarity, string rewardTemplateName, decimal randomValueRangeBegin, decimal randomValueRangeEnd, bool excludeFromRandomization = false,bool excludeFromLootboxRandomization = false)
        {
            if (currencyKey.CurrencyId == Guid.Empty || randomValueRangeEnd < randomValueRangeBegin || string.IsNullOrWhiteSpace(rewardTemplateName))
            {
                throw new ArgumentException();
            }

            return new RewardTemplateEntity
            {
                RewardTemplateId = this._guidGenerator.GenerateGuid(),
                CurrencyKey = currencyKey,
                Rarity = rarity,
                Name = rewardTemplateName,
                AmountRangeBegin = randomValueRangeBegin,
                AmountRangeEnd = randomValueRangeEnd,
                ExcludeFromRandomization = excludeFromRandomization,
                ExcludeFromLootboxRandomization = excludeFromLootboxRandomization
            };
        }
    }
}