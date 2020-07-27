using System;

namespace Dapper.WebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}