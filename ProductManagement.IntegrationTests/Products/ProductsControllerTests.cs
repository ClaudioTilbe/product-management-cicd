using FluentAssertions;
using ProductManagement.API.DTOs;
using ProductManagement.IntegrationTests.DTOs;
using ProductManagement.IntegrationTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ProductManagement.IntegrationTests.Products
{
    public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly HttpClient _client;


        public ProductsControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }


        private StringContent CreateJsonContent(object data)
        {
            return new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );
        }


        private async Task<int> CreateProductAsync()
        {
            var product = ProductTestData.CreateValidProduct();

            var response = await _client.PostAsync(
                ProductEndpoints.Base,
                CreateJsonContent(product));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var created =
     await response.Content.ReadFromJsonAsync<ProductResponseDTO>();

            created.Should().NotBeNull();

            return created!.Id;
        }


        // Verifica que la API devuelve correctamente la lista de productos.
        [Fact]
        public async Task GetProducts_ShouldReturnOk()
        {

            // Arrange
            var endpoint = ProductEndpoints.Base;

            // Act
            var response = await _client.GetAsync(endpoint);

            // Assert
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

        }


        // Verifica que la API crea correctamente un producto válido.
        [Fact]
        public async Task CreateProduct_ShouldReturnCreated_WhenProductIsValid()
        {

            // Arrange
            var product = ProductTestData.CreateValidProduct();

            // Act
            var response =
                await _client.PostAsync(
                    ProductEndpoints.Base,
                    CreateJsonContent(product)
                );

            // Assert
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            var created =
              await response.Content.ReadFromJsonAsync<ProductResponseDTO>();

            created.Should().NotBeNull();

            created!.Id.Should().BeGreaterThan(0);

            created.Name.Should().Be(product.Name);

            created.Description.Should().Be(product.Description);

            created.Price.Should().Be(product.Price);

            created.Stock.Should().Be(product.Stock);

            created.IsActive.Should().BeTrue();

            var getResponse =
                await _client.GetAsync(
                    ProductEndpoints.ById(created.Id)
                );

            getResponse.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

        }



        // Verifica que la API devuelve un producto existente correctamente.
        [Fact]
        public async Task GetProductById_ShouldReturnOk_WhenProductExists()
        {

            // Arrange
            var productId = await CreateProductAsync();

            // Act
            var response =
                await _client.GetAsync(
                    ProductEndpoints.ById(productId)
                );

            var product =
                await response.Content.ReadFromJsonAsync<ProductResponseDTO>();

            // Assert
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            product
                .Should()
                .NotBeNull();

            product!.Id
                .Should()
                .Be(productId);

            product.Name
                .Should()
                .NotBeNullOrEmpty();

        }



        // Verifica que la API devuelve NotFound cuando el producto no existe.
        [Fact]
        public async Task GetProductById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {

            // Arrange
            const int productId = 999;

            // Act
            var response =
                await _client.GetAsync(
                    ProductEndpoints.ById(productId)
                );

            // Assert
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);

        }



        // Verifica que la API actualiza correctamente un producto existente.
        [Fact]
        public async Task UpdateProduct_ShouldUpdateProduct_WhenProductExists()
        {

            // Arrange
            var productId = await CreateProductAsync();

            var product = ProductTestData.CreateUpdateProduct();

            // Act
            var response =
                await _client.PutAsync(
                    ProductEndpoints.ById(productId),
                    CreateJsonContent(product)
                );

            // Assert
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.NoContent);

            var getResponse =
                await _client.GetAsync(
                    ProductEndpoints.ById(productId)
                );

            getResponse.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            var updatedProduct =
                await getResponse.Content
                    .ReadFromJsonAsync<ProductResponseDTO>();

            updatedProduct
                .Should()
                .NotBeNull();

            updatedProduct!.Name
                .Should()
                .Be(product.Name);

            updatedProduct.Description
                .Should()
                .Be(product.Description);

            updatedProduct.Price
                .Should()
                .Be(product.Price);

            updatedProduct.Stock
                .Should()
                .Be(product.Stock);

            updatedProduct.IsActive
                .Should()
                .Be(product.IsActive);

        }



        // Verifica que la API elimina correctamente un producto existente.
        [Fact]
        public async Task DeleteProduct_ShouldDeleteProduct_WhenProductExists()
        {

            // Arrange
            var productId = await CreateProductAsync();

            // Act
            var response =
                await _client.DeleteAsync(
                    ProductEndpoints.ById(productId)
                );

            // Assert
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.NoContent);

            var getResponse =
                await _client.GetAsync(
                    ProductEndpoints.ById(productId)
                );

            getResponse.StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);

        }





    }



}
