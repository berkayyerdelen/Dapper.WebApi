using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services.Queries
{
    public class CommandText:ICommandText
    {
        public string GetProducts => "Select * from Product";
        public string GetProductById => "Select * from Product where Id= @Id";
        public string AddProduct => "Insert into  [Dapper].[dbo].[Product] ([Name], Cost, CreatedDate) values (@Name, @Cost, @CreatedDate)";
        public string UpdateProduct => "Update [Dapper].[dbo].[Product] set Name = @Name, Cost = @Cost, CreatedDate = GETDATE() where Id =@Id";
        public string RemoveProduct => "Delete from [Dapper].[dbo].[Product] where Id= @Id";
        public string GetProductByIdSp => "GetProductId";

    }
}
