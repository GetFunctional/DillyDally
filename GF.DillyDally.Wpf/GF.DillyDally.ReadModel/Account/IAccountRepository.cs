using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;

namespace GF.DillyDally.ReadModel.Account
{
    public interface IAccountRepository
    {
        Task<IList<IAccountBalanceEntity>> GetAllAccounts();
    }
}