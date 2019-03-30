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
    }
}