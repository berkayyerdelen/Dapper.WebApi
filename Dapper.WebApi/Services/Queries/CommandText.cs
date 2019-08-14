using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services.Queries
{
    public class CommandText:ICommandText
    {
        public string GetProductById => "Select * from Product";
    }
}
