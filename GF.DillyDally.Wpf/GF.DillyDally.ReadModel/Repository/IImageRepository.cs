using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    public interface IImageRepository : IRepository<ImageEntity>
    {
        Task<IList<ImageEntity>> GetByOriginalFileIdAsync(IDbConnection connection, Guid fileId);

        Task<Guid> GetPreviewImageIdForFileAsync(IDbConnection connection, Guid fileId);
    }
}