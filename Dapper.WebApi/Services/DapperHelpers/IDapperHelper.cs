using Dapper.WebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services.DapperHelpers
{
    public interface IDapperHelper
    {
        Task<IEnumerable<T>> GetAllProducts<T>(IDbConnection connection, string commandText);
        ValueTask<T> GetById<T>(IDbConnection connection, int id, string commandText);
        Task AddProduct(IDbConnection connection, Product entity, string commandText);
        Task UpdateProduct(IDbConnection connection, Product entity, int id, string commandText);
        Task RemoveProduct(IDbConnection connection, int id, string commandText);
    }
}
