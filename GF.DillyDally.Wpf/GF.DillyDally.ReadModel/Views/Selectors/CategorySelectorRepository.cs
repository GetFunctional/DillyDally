using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.ReadModel.Projection.Categories.Repository;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;

namespace GF.DillyDally.ReadModel.Views.Selectors
{
    public sealed class CategorySelectorRepository
    {
        public async Task<IList<CategorySelectorEntity>> GetAllCategoriesAsync(IDbConnection connection)
        {
            var sql =
                $"SELECT {nameof(CategoryEntity.CategoryId)}, {nameof(CategoryEntity.Name)}, {nameof(CategoryEntity.ColorCode)}, {nameof(RunningNumberEntity.RunningNumber)} " +
                $"FROM {CategoryEntity.TableNameConstant} " +
                $"JOIN {RunningNumberEntity.TableNameConstant} ON {RunningNumberEntity.TableNameConstant}.{nameof(RunningNumberEntity.RunningNumberId)} = {CategoryEntity.TableNameConstant}.{nameof(CategoryEntity.RunningNumberId)};";

            return (await connection.QueryAsync<CategorySelectorEntity>(sql)).ToList();
        }
    }
}