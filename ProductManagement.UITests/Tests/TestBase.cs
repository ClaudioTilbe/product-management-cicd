using OpenQA.Selenium.Support.UI;
using ProductManagement.UITests.Configuration;
using ProductManagement.UITests.Fixtures;
using ProductManagement.UITests.Helpers;

namespace ProductManagement.UITests.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected SeleniumFixture Fixture { get; }
        protected DatabaseFixture Database { get; }
        protected int? CreatedProductId { get; set; }



        protected string BaseUrl => TestConfiguration.BaseUrl;



        protected TestBase(BrowserType browser)
        {
            Fixture = new SeleniumFixture(browser);
            Database = new DatabaseFixture();
        }



        protected void Cleanup()
        {
            if (CreatedProductId.HasValue)
            {
                DeleteCreatedProduct(CreatedProductId.Value);
            }
        }


        private void DeleteCreatedProduct(int productId)
        {
            var cleanup = new TestDataCleanup(Database.Context);
            cleanup.DeleteProduct(productId);
            CreatedProductId = null;
        }

       

        protected void WaitForUrlContains(string text)
        {
            var wait = new WebDriverWait(Fixture.Driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.Url.Contains(text));
        }




        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            // try/finally garantiza que el navegador siempre se cierra
            // aunque Cleanup lance una excepción
            try
            {
                Cleanup();
            }
            finally
            {
                Fixture?.Dispose();
                Database?.Dispose();
            }
        }



    }


}
