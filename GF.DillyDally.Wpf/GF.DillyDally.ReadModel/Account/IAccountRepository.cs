using System.Collections.Generic;
using System.Threading.Tasks;

namespace GF.DillyDally.ReadModel.Account
{
    public interface IAccountRepository
    {
        Task<IList<AccountEntity>> GetAllAccounts();
    }
}