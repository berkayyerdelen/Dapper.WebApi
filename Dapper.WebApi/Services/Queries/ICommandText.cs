using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services.Queries
{
    public interface ICommandText
    {
        string GetProducts { get; }
        string GetProductById { get; }
        string AddProduct { get; }
        string UpdateProduct { get; }
        string RemoveProduct { get; }
        string GetProductByIdSp { get; }
    }
}
