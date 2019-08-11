using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Core;
using GF.DillyDally.WriteModel.Games.Aggregates.Commands;

namespace GF.DillyDally.WriteModel.Games.Aggregates
{
    public interface IGameCollectionService : IDomainService
    {
        Task<CreateShelfResponse> CreateNewShelfAsync(string shelfName);
    }
}