using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    public interface ICurrencyService
    {
        Task<CurrencyKey> CreateCurrencyAsync(string name, string code);
    }
}