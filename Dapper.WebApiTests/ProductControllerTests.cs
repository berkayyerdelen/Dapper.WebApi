using Dapper.WebApi.Controllers;
using Dapper.WebApi.Models;
using Dapper.WebApi.Services;
using DeepEqual.Syntax;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.WebApiTests
{
    public class ProductControllerTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductController _productController;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productController = new ProductController(_productRepositoryMock.Object);
        }

        [Test]
        public void GellAll_GiveValidData_ReturnValidData()
        {
            // Arrange.
            IEnumerable<Product> productsTestData = new List<Product>
            {
                new Product { Id = 1, Name = "P1", Cost = 10.5, CreatedDate = DateTime.Now.Date },
                new Product { Id = 2, Name = "P2", Cost = 20.3, CreatedDate = DateTime.Now.Date.AddDays(-1) }
            };
            _productRepositoryMock.Setup(m => m.GetAllProducts()).Returns(Task.FromResult(productsTestData));

            // Act.
            IEnumerable<Product> productsFromController
                = (IEnumerable<Product>)((OkObjectResult)_productController.GellAll().Result.Result).Value;

            // Assert.
            productsFromController.ShouldDeepEqual(productsTestData);
        }

        [Test]
        public void GellById_GiveValidData_ReturnValidData()
        {
            // Arrange.
            var productTestData = new Product { Id = 1, Name = "P1", Cost = 10.5, CreatedDate = DateTime.Now.Date };
            var productId = 1;
            _productRepositoryMock.Setup(m => m.GetById(productId)).Returns(new ValueTask<Product>(productTestData));

            // Act.
            Product productFromController
                = (Product)((OkObjectResult)_productController.GetById(productId).Result.Result).Value;

            // Assert.
            productFromController.ShouldDeepEqual(productTestData);
        }

        [Test]
        public void Verify_Add()
        {
            // Arrange.
            var productTestData = new Product { Id = 1, Name = "P1", Cost = 10.5, CreatedDate = DateTime.Now.Date };
            _productRepositoryMock.Setup(m => m.AddProduct(productTestData)).Verifiable();

            // Act.
            ActionResult taskFromAdd = _productController.AddProduct(productTestData).Result;

            // Assert.
            _productRepositoryMock.VerifyAll();
        }

        [Test]
        public void Verify_Update()
        {
            // Arrange.
            var productTestData = new Product { Id = 1, Name = "P1", Cost = 10.5, CreatedDate = DateTime.Now.Date };
            var productId = 1;
            _productRepositoryMock.Setup(m => m.UpdateProduct(productTestData, productId)).Verifiable();

            // Act.
            ActionResult<Product> taskFromAdd = _productController.Update(productTestData, productId).Result;

            // Assert.
            _productRepositoryMock.VerifyAll();
        }

        [Test]
        public void Verify_Delete()
        {
            // Arrange.
            var productId = 1;
            _productRepositoryMock.Setup(m => m.RemoveProduct(productId)).Verifiable();

            // Act.
            ActionResult<Product> taskFromAdd = _productController.Delete(productId).Result;

            // Assert.
            _productRepositoryMock.VerifyAll();
        }
    }
}