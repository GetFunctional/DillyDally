using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;

namespace GF.DillyDally.WriteModel.Domain.Files
{
    internal class FileRepository : Repository<FileEntity>
    {
        public async Task<bool> HasFileWithSameHashAsync(IDbConnection connection, string md5HashString)
        {
            var sql =
                $"SELECT {nameof(FileEntity.FileId)} FROM {FileEntity.TableNameConstant} WHERE {nameof(FileEntity.Md5Hash)} = @md5Hash;";
            var result = await connection.QueryFirstOrDefaultAsync<FileEntity>(sql, new {md5Hash = md5HashString});

            return result != null && result.FileId != Guid.Empty;
        }


        public async Task<FileEntity> GetFileByMd5HashAsync(IDbConnection connection, string md5HashString)
        {
            var sql =
                $"SELECT * FROM {FileEntity.TableNameConstant} WHERE {nameof(FileEntity.Md5Hash)} = @md5Hash;";
            var result = await connection.QueryFirstOrDefaultAsync<FileEntity>(sql, new {md5Hash = md5HashString});

            return result;
        }
    }
}