using System.Collections.Generic;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Common
{
    public interface ICommonDataRepository
    {
        Task<IList<CurrencyEntity>> GetAllCurrencies();
    }
}