using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services.Queries
{
    public class CommandText:ICommandText
    {
        public string GetProducts => "Select * from Products";
        public string GetProductById => "Select * from Products where Id= @Id";
        public string AddProduct => "Insert into  [Dapper].[dbo].[Products] ([Name], Cost, CreatedDate) values (@Name, @Cost, @CreatedDate)";
        public string UpdateProduct => "Update [Dapper].[dbo].[Products] set Name = @Name, Cost = @Cost, CreatedDate = GETDATE() where Id =@Id";
        public string RemoveProduct => "Delete from [Dapper].[dbo].[Products] where Id= @Id";
    }
}
