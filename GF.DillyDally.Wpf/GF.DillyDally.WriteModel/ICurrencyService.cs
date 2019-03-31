using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.WriteModel
{
    public interface ICurrencyService
    {
        Task<CurrencyKey> CreateCurrencyAsync(string name, string code);
    }
}