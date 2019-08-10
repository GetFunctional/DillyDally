using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Core;
using GF.DillyDally.WriteModel.Files.Commands;

namespace GF.DillyDally.WriteModel.Files
{
    public interface IFileService : IDomainService
    {
        Task<StoreFileResponse> GetOrCreateFileAsync(string filePath);

        Task<StoreFileResponse> GetOrCreateFileAsync(byte[] binary);
    }
}