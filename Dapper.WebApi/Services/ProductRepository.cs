using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper.WebApi.Extensions;
using Dapper.WebApi.Models;
using Dapper.WebApi.Services.ExecuteCommands;
using Dapper.WebApi.Services.Queries;
using Microsoft.Extensions.Configuration;

namespace Dapper.WebApi.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ICommandText _commandText;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        public ProductRepository(IConfiguration configuration, ICommandText commandText, IExecuters executers)
        {
            _commandText = commandText;
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("Dapper");
            _executers = executers;
        }


        public List<Product> GetAllProducts()
        {
            var query = _executers.ExecuteCommand(_connStr,
                   conn => conn.Query<Product>(_commandText.GetProducts)).ToList();
            return query;
        }
        public Product GetById(int id)
        {
            var product = _executers.ExecuteCommand<Product>(_connStr, conn =>
                conn.Query<Product>(_commandText.GetProductById, new { @Id = id }).SingleOrDefault());
            return product;
        }
        public void AddProduct(Product entity)
        {
            _executers.ExecuteCommand(_connStr, conn => {
                var query = conn.Query<Product>(_commandText.AddProduct,
                    new { Name = entity.Name, Cost = entity.Cost, CreatedDate = entity.CreatedDate });
            });
        }
        public void UpdateProduct(Product entity, int id)
        {
            _executers.ExecuteCommand(_connStr, conn =>
            {
                var query = conn.Query<Product>(_commandText.UpdateProduct,
                    new { Name = entity.Name, Cost = entity.Cost, Id = id });
            });
        }

        public void RemoveProduct(int id)
        {
            _executers.ExecuteCommand(_connStr, conn =>
            {
                var query = conn.Query<Product>(_commandText.RemoveProduct, new { Id = id });
            });
        }


     
    }
}
