using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Core;
using GF.DillyDally.WriteModel.Files.Aggregates.Files.Commands;

namespace GF.DillyDally.WriteModel.Files.Aggregates
{
    public interface IFileService : IDomainService
    {
        Task<StoreFileResponse> GetOrCreateFileAsync(string filePath);

        Task<StoreFileResponse> GetOrCreateFileAsync(byte[] binary);
    }
}