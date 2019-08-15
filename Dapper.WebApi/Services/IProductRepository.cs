using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.WebApi.Models;

namespace Dapper.WebApi.Services
{
    public interface IProductRepository
    {
        Product GetById(int id);
        void AddProduct(Product entity);
        void UpdateProduct(Product entity, int id);
        void RemoveProduct(int id);
        List<Product> GetAllProducts();
    }
}
