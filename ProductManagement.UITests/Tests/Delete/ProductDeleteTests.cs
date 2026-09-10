using FluentAssertions;
using ProductManagement.UITests.Configuration;
using ProductManagement.UITests.Fixtures;
using ProductManagement.UITests.Helpers;
using ProductManagement.UITests.Pages;
using ProductManagement.UITests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Tests.Delete
{
    public abstract class ProductDeleteTests : TestBase
    {

        protected ProductDeleteTests(BrowserType browser) : base(browser) { }



        [Fact]
        public void DeleteProduct_ShouldRemoveProduct_WhenConfirmed()
        {

            // Arrange
            var builder = new TestDataBuilder(Database.Context);

            CreatedProductId = builder.CreateProduct(ProductTestFactory.CreateValidProduct());


            var page = NavigateToProductList();

            page.ProductExists(CreatedProductId.Value)
                .Should()
                .BeTrue(because: "el producto debe existir en el listado antes de intentar eliminarlo");


            // Act
            page.ClickDeleteProduct(CreatedProductId.Value);
            page.AcceptDeleteConfirmation();
            page.WaitUntilProductIsDeleted(CreatedProductId.Value);


            // Assert
            page.ProductExists(CreatedProductId.Value)
                .Should()
                .BeFalse(because: "el producto debe desaparecer del listado tras confirmar su eliminación");
        }




        // Helpers privados 

        private ProductListPage NavigateToProductList()
        {
            Fixture.Driver
                .Navigate()
                .GoToUrl($"{BaseUrl}/products");

            return new ProductListPage(Fixture.Driver);
        }



    }


}
