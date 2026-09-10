using FluentAssertions;
using ProductManagement.UITests.Configuration;
using ProductManagement.UITests.Fixtures;
using ProductManagement.UITests.Helpers;
using ProductManagement.UITests.Models;
using ProductManagement.UITests.Pages;
using ProductManagement.UITests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Tests.Edit
{
    public abstract class ProductEditTests : TestBase
    {

        protected ProductEditTests(BrowserType browser) : base(browser) { }


        [Fact]
        public void EditProduct_ShouldRedirectToList_WhenUpdateIsValid()
        {
            // Arrange
            var builder = new TestDataBuilder(Database.Context);

            CreatedProductId = builder.CreateProduct(ProductTestFactory.CreateValidProduct());


            CreatedProductId
                .Should()
                .BeGreaterThan(
                    0,
                    because: "el producto debe haberse creado correctamente en base de datos antes de editarlo"
                );

            var page = NavigateToEditPage(CreatedProductId.Value);

            page.IsLoaded()
                .Should()
                .BeTrue(because: "el formulario de edición debe estar visible antes de interactuar");


            // Act
            page.UpdateProduct(ProductTestFactory.CreateUpdatedProduct());


            // Assert
            Fixture.Driver.Url
                .Should()
                .Contain(
                    "/products",
                    because: "tras guardar una edición válida debe volver al listado de productos"
                );
        }



        // Helpers privados 

        private ProductEditPage NavigateToEditPage(int productId)
        {
            Fixture.Driver
                .Navigate()
                .GoToUrl($"{BaseUrl}/products/edit/{productId}");

            return new ProductEditPage(Fixture.Driver);
        }
    }




}
