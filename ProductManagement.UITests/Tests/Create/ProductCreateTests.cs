using FluentAssertions;
using ProductManagement.UITests.Configuration;
using ProductManagement.UITests.Pages;
using ProductManagement.UITests.TestData;


namespace ProductManagement.UITests.Tests.Create
{

    public abstract class ProductCreateTests : TestBase
    {


        // Constructor en lugar de propiedad abstracta + Setup()
        protected ProductCreateTests(BrowserType browser) : base(browser) { }



        [Fact]
        public void CreateProduct_ShouldRedirectToDetailWithValidId()
        {
            var page = NavigateToCreatePage();


            page.IsLoaded()
                .Should()
                .BeTrue(because: "el formulario de creación debe estar visible antes de interactuar");

            //Creo Producto
            page.CreateProduct(ProductTestFactory.CreateValidProduct());

            //Espero URL Detail
            WaitForUrlContains("/products/detail/");


            Fixture.Driver.Url
                .Should()
                .MatchRegex(
                    @".*/products/detail/\d+$",
                    because: "crear un producto válido debe redirigir a su página de detalle"
                );


            //Extraigo ID
            var productId = ExtractProductIdFromUrl();


            productId
                .Should()
                .BeGreaterThan(
                    0,
                    because: "el servidor debe asignar un ID positivo al producto creado"
                );


            //Asigno ID para Cleanup
            CreatedProductId = productId;
        }



        // Helpers privados



        private ProductCreatePage NavigateToCreatePage()
        {
            Fixture.Driver
                .Navigate()
                .GoToUrl($"{BaseUrl}/products/create");

            return new ProductCreatePage(Fixture.Driver);
        }



        private int ExtractProductIdFromUrl()
        {
            var segment = Fixture.Driver.Url.Split('/')[^1];

            int.TryParse(segment, out var productId)
                .Should()
                .BeTrue(
                    because: $"el último segmento de la URL debe ser un ID numérico, pero fue '{segment}'"
                );

            return productId;
        }
    }



}
