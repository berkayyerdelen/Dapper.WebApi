using Dapper.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services
{
    public interface IProductRepository
    {
        ValueTask<Product> GetById(int id);
        Task AddProduct(Product entity);
        Task UpdateProduct(Product entity, int id);
        Task RemoveProduct(int id);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
