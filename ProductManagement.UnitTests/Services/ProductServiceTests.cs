using FluentAssertions;
using Moq;
using ProductManagement.API.DTOs;
using ProductManagement.API.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Repositories;
using ProductManagement.UnitTests.Fixtures;



namespace ProductManagement.UnitTests.Services
{
    public class ProductServiceTests
    {

        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly ProductService _service;


        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _service = new ProductService(_repositoryMock.Object);
        }



        // Verifica que el servicio retorna un producto cuando existe un registro con el ID solicitado.
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = ProductFixture.Create();

            _repositoryMock
                .Setup(x => x.GetByIdAsync(product.Id))
                .ReturnsAsync(product);

            // Act
            var result = await _service.GetByIdAsync(product.Id);

            // Assert
            result.Should().NotBeNull();

            result.Should()
                .BeEquivalentTo(product, options =>
                    options.ExcludingMissingMembers());

        }



        // Verifica que el servicio retorna null cuando se solicita un producto con un ID inexistente.
        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            const int productId = 99;

            _repositoryMock
                .Setup(x => x.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _service.GetByIdAsync(productId);

            // Assert
            result.Should().BeNull();

            _repositoryMock
                .Verify(
                    x => x.GetByIdAsync(productId),
                    Times.Once);

        }



        // Verifica que el servicio retorna correctamente la lista de productos cuando existen registros.
        [Fact]
        public async Task GetAllAsync_ShouldReturnProductList_WhenProductsExist()
        {

            // Arrange
            var products = ProductFixture.CreateList();

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();

            result.Should().HaveCount(products.Count);

            result.First().Name
                .Should()
                .Be(products.First().Name);

            _repositoryMock
                .Verify(
                    x => x.GetAllAsync(),
                    Times.Once);

        }



        // Verifica que el servicio crea correctamente un producto cuando recibe datos válidos.
        [Fact]
        public async Task CreateAsync_ShouldCreateProduct_WhenDataIsValid()
        {
            // Arrange
            var dto = ProductFixture.CreateValidCreateDto();

            Product? createdProduct = null;

            _repositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Product>()))
                .Callback<Product>(product =>
                {
                    product.Id = 1;

                    createdProduct = product;

                })
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();

            result.Id
                .Should()
                .Be(1);

            result.Name
                .Should()
                .Be(dto.Name);

            result.Description
                .Should()
                .Be(dto.Description);

            result.Price
                .Should()
                .Be(dto.Price);

            result.Stock
                .Should()
                .Be(dto.Stock);

            result.IsActive
                .Should()
                .BeTrue();

            createdProduct.Should().NotBeNull();

            createdProduct!.Name
                .Should()
                .Be(dto.Name);

            createdProduct.Price
                .Should()
                .Be(dto.Price);

            createdProduct.Stock
                .Should()
                .Be(dto.Stock);

            _repositoryMock.Verify(
                x => x.AddAsync(
                    It.Is<Product>(
                        p =>
                            p.Name == dto.Name &&
                            p.Price == dto.Price &&
                            p.Stock == dto.Stock)),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);

        }



        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenPriceIsInvalid()
        {
            // Arrange
            var dto = ProductFixture.CreateInvalidPriceDto();

            // Act
            Func<Task> action =
                async () =>
                {
                    await _service.CreateAsync(dto);
                };

            // Assert
            await action
                .Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("Price must be greater than zero.");

        }



        // Verifica que el servicio rechaza la creación de un producto cuando el stock es negativo.
        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenStockIsNegative()
        {

            // Arrange
            var dto = ProductFixture.CreateInvalidStockDto();

            // Act
            Func<Task> action =
                async () =>
                {
                    await _service.CreateAsync(dto);
                };

            // Assert
            await action
                .Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("Stock cannot be negative.");

        }


        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct_WhenProductExists()
        {

            // Arrange
            var product = ProductFixture.CreateForUpdate();
            var dto = ProductFixture.CreateUpdateDto();

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            _repositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            result.Should()
                .BeTrue();

            product.Name
             .Should()
             .Be(dto.Name);


            product.Price
                .Should()
                .Be(dto.Price);

            _repositoryMock
                .Verify(
                    x => x.UpdateAsync(product),
                    Times.Once);

        }



        // Verifica que el servicio retorna false cuando se intenta actualizar un producto inexistente.
        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {

            // Arrange
            var productId = 99;

            var dto = ProductFixture.CreateUpdateDto();

            _repositoryMock
                .Setup(x => x.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _service.UpdateAsync(productId, dto);

            // Assert
            result.Should().BeFalse();

            _repositoryMock
                .Verify(
                    x => x.UpdateAsync(It.IsAny<Product>()),
                    Times.Never);

            _repositoryMock
                .Verify(
                    x => x.SaveChangesAsync(),
                    Times.Never);

        }



        // Verifica que el servicio elimina correctamente un producto existente.
        [Fact]
        public async Task DeleteAsync_ShouldDeleteProduct_WhenExists()
        {

            // Arrange
            var product = ProductFixture.Create();
            var productId = product.Id;

            _repositoryMock
                .Setup(x => x.GetByIdAsync(product.Id))
                .ReturnsAsync(product);

            _repositoryMock
                .Setup(x => x.DeleteAsync(product))
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(productId);

            // Assert
            result.Should().BeTrue();

            _repositoryMock
                .Verify(
                    x => x.DeleteAsync(product),
                    Times.Once);

            _repositoryMock
                .Verify(
                    x => x.SaveChangesAsync(),
                    Times.Once);

        }


        // Verifica que el servicio retorna false cuando se intenta eliminar un producto inexistente.
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {

            // Arrange
            var productId = 50;

            _repositoryMock
                .Setup(x => x.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act

            var result =
                await _service.DeleteAsync(productId);

            // Assert

            result.Should()
                .BeFalse();

            _repositoryMock
                .Verify(
                    x => x.DeleteAsync(It.IsAny<Product>()),
                    Times.Never);

            _repositoryMock
                .Verify(
                    x => x.SaveChangesAsync(),
                    Times.Never);

        }




    }
}
