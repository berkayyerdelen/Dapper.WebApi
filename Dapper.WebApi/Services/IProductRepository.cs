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
        Task AddProduct(Product entity);
    }
}
