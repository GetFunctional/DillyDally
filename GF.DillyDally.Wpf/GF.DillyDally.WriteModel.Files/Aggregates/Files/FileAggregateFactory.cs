using GF.DillyDally.WriteModel.Files.Aggregates.Files.Filestore;

namespace GF.DillyDally.WriteModel.Files.Aggregates.Files
{
    internal class FileAggregateFactory
    {
        internal FileAggregateRoot CreateFile(FileData fileDataToStore)
        {
            var aggregate = FileAggregateRoot.Create(fileDataToStore.FileId, fileDataToStore.Name,
                fileDataToStore.Size, fileDataToStore.Md5Hash,
                fileDataToStore.Extension);

            return aggregate;
        }
    }
}