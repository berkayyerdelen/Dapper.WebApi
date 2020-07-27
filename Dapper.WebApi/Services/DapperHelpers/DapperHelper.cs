using Dapper.WebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services.DapperHelpers
{
    public class DapperHelper : IDapperHelper
    {   
        async Task<IEnumerable<T>> IDapperHelper.GetAllProducts<T>(IDbConnection connection, string commandText)
        {
            var query = await connection.QueryAsync<T>(commandText);
            return query;
        }

        async ValueTask<T> IDapperHelper.GetById<T>(IDbConnection connection, int id, string commandText)
        {
            var query = await connection.QueryFirstOrDefaultAsync<T>(commandText, new { Id = id });
            return query;
        }

        async Task IDapperHelper.AddProduct(IDbConnection connection, Product entity, string commandText)
        {
            await connection.ExecuteAsync(commandText,
                new { Name = entity.Name, Cost = entity.Cost, CreatedDate = entity.CreatedDate });
        }

        async Task IDapperHelper.UpdateProduct(IDbConnection connection, Product entity, int id, string commandText)
        {
            await connection.ExecuteAsync(commandText,
                new { Name = entity.Name, Cost = entity.Cost, Id = id });
        }

        async Task IDapperHelper.RemoveProduct(IDbConnection connection, int id, string commandText)
        {
            await connection.ExecuteAsync(commandText, new { Id = id });
        }
    }
}
