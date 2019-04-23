using System;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Data.Sqlite.Deprecated;

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

        public RewardEntity CreateReward(CurrencyKey currencyKey, Rarity rarity,
            string rewardTemplateName, decimal randomValueRangeBegin, decimal randomValueRangeEnd,
            bool excludeFromRandomization = false, bool excludeFromLootboxRandomization = false)
        {
            if (currencyKey.CurrencyId == Guid.Empty || randomValueRangeEnd < randomValueRangeBegin ||
                string.IsNullOrWhiteSpace(rewardTemplateName))
            {
                throw new ArgumentException();
            }

            return new RewardEntity
            {
                RewardId = this._guidGenerator.GenerateGuid(),
                CurrencyKey = currencyKey,
                Rarity = rarity,
                Name = rewardTemplateName,
                AmountRangeBegin = randomValueRangeBegin,
                AmountRangeEnd = randomValueRangeEnd,
                ExcludeFromRandomization = excludeFromRandomization,
                ExcludeFromLootboxRandomization = excludeFromLootboxRandomization
            };
        }

        public TaskEntity CreateTaskEntity(string initialName, TaskType taskType)
        {
            if (string.IsNullOrWhiteSpace(initialName))
            {
                throw new ArgumentException();
            }

            return new TaskEntity
            {
                TaskId = this._guidGenerator.GenerateGuid(),
                Name = initialName,
                TaskType = taskType,
                CreatedOn = DateTime.Now
            };
        }

        public TaskEntity CreateTaskEntityForInsert(TaskKey taskKey, string taskDescription, TaskType taskType,
            DateTime? taskDueDate, string taskName)
        {
            return new TaskEntity
            {
                TaskId = taskKey.TaskId,
                Name = taskName,
                TaskType = taskType,
                CreatedOn = DateTime.Now,
                Description = taskDescription
            };
        }

        public TaskRewardEntity CreateTaskReward(RewardKey rewardKey, TaskRewardKey taskRewardKey, TaskKey taskKey,
            int amount)
        {
            return new TaskRewardEntity
            {
                TaskKey = taskKey,
                Amount = amount,
                RewardKey = rewardKey,
                TaskRewardId = taskRewardKey.TaskRewardId
            };
        }
    }
}