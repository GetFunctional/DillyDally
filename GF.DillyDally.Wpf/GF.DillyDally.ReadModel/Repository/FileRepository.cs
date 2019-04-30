using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class FileRepository : Repository<FileEntity>, IFileRepository
    {
        public FileRepository(DatabaseFileHandler fileHandler) : base(fileHandler)
        {
        }


        

        //using (var md5 = MD5.Create())
        //{
        //using (var stream = File.OpenRead(filename))
        //{
        //    return md5.ComputeHash(stream);
        //}
        //}

        //static string CalculateMD5(string filename)
        //{
        //    using (var md5 = MD5.Create())
        //    {
        //        using (var stream = File.OpenRead(filename))
        //        {
        //            var hash = md5.ComputeHash(stream);
        //            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        //        }
        //    }
        //}
    }
}