using Dapper.WebApi.Models;
using Dapper.WebApi.Services;
using Dapper.WebApi.Services.DapperHelpers;
using Dapper.WebApi.Services.Queries;
using DeepEqual.Syntax;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Dapper.WebApiTests
{
    [TestFixture]
    internal class ProductRepositoryTests
    {
        private Mock<ICommandText> _commandTextMock;
        private Mock<DbConnection> _dbConnectionMock;
        private Mock<IDapperHelper> _dapperHelperMock;
        private ProductRepository _productRepository;

        [SetUp]
        public void Setup()
        {
            _commandTextMock = new Mock<ICommandText>();

            _dbConnectionMock = new Mock<DbConnection>();
            _dbConnectionMock.SetupGet(connection => connection.ConnectionString).Returns("TestConnectionString");

            _dapperHelperMock = new Mock<IDapperHelper>();

            _productRepository = new ProductRepository(_commandTextMock.Object, _dbConnectionMock.Object, _dapperHelperMock.Object);
        }

        [Test]
        public void GellAll_GiveValidData_ReturnValidData()
        {
            // Arrange.
            _commandTextMock.SetupGet(commandText => commandText.GetProducts).Returns("Select all command text");
            IEnumerable<Product> testProducts = new List<Product>
            {
                new Product { Id = 1, Name = "p1", Cost = 10.5, CreatedDate = DateTime.Now.AddDays(30) },
                new Product { Id = 2, Name = "p2", Cost = 20.6, CreatedDate = DateTime.Now.AddDays(60) }
            };
            _dapperHelperMock
                .Setup(dapperHelper => dapperHelper.GetAllProducts<Product>(It.IsAny<DbConnection>(), It.IsAny<string>()))
                .Returns(Task.FromResult(testProducts));

            // Act.
            IEnumerable<Product> products = _productRepository.GetAllProducts().Result;

            // Assert.
            products.ShouldDeepEqual(testProducts);
        }

        [Test]
        public void GellById_GiveValidData_ReturnValidData()
        {
            // Arrange.
            _commandTextMock.SetupGet(commandText => commandText.GetProducts).Returns("Select by id command text");
            var testProduct = new Product { Id = 1, Name = "p1", Cost = 10.5, CreatedDate = DateTime.Now.AddDays(30) };
            _dapperHelperMock
                .Setup(dapperHelper => dapperHelper.GetById<Product>(It.IsAny<DbConnection>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new ValueTask<Product>(testProduct));

            // Act.
            Product product = _productRepository.GetById(It.IsAny<int>()).Result;

            // Assert.
            product.ShouldDeepEqual(testProduct);
        }

        [Test]
        public void Verify_Add()
        {
            // Arrange.
            _commandTextMock.SetupGet(commandText => commandText.GetProducts).Returns("Insert command text");
            _dapperHelperMock
                .Setup(dapperHelper => dapperHelper.AddProduct(It.IsAny<DbConnection>(), It.IsAny<Product>(), It.IsAny<string>()))
                .Verifiable();

            // Act.
            _productRepository.AddProduct(It.IsAny<Product>()).Wait();

            // Assert.
            _dapperHelperMock.Verify();
        }

        [Test]
        public void Verify_Update()
        {
            // Arrange.
            _commandTextMock.SetupGet(commandText => commandText.GetProducts).Returns("Update command text");
            _dapperHelperMock
                .Setup(dapperHelper => dapperHelper.UpdateProduct(It.IsAny<DbConnection>(), It.IsAny<Product>(), It.IsAny<int>(), It.IsAny<string>()))
                .Verifiable();

            // Act.
            _productRepository.UpdateProduct(It.IsAny<Product>(), It.IsAny<int>()).Wait();

            // Assert.
            _dapperHelperMock.Verify();
        }

        [Test]
        public void Verify_Remove()
        {
            // Arrange.
            _commandTextMock.SetupGet(commandText => commandText.GetProducts).Returns("Update command text");
            _dapperHelperMock
                .Setup(dapperHelper => dapperHelper.RemoveProduct(It.IsAny<DbConnection>(), It.IsAny<int>(), It.IsAny<string>()))
                .Verifiable();

            // Act.
            _productRepository.RemoveProduct(It.IsAny<int>()).Wait();

            // Assert.
            _dapperHelperMock.Verify();
        }
    }
}
