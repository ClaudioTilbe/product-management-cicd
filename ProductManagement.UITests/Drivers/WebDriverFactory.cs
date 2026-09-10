using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using ProductManagement.UITests.Configuration;



namespace ProductManagement.UITests.Drivers
{
    public static class WebDriverFactory
    {

        public static IWebDriver Create(BrowserType browser)
        {
            // Si hay grid configurado, usamos RemoteWebDriver
            if (TestConfiguration.UseGrid)
            {
                return CreateRemote(browser);
            }

            return browser switch
            {
                BrowserType.Chrome => CreateChrome(),
                BrowserType.Edge => CreateEdge(),
                _ => throw new ArgumentException()
            };
        }



        private static IWebDriver CreateChrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }



        private static IWebDriver CreateEdge()
        {
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            return new EdgeDriver(options);
        }



        // Para CI con Selenium Grid
        private static IWebDriver CreateRemote(BrowserType browser)
        {
            DriverOptions options = browser switch
            {
                BrowserType.Chrome => new ChromeOptions(),
                BrowserType.Edge => new EdgeOptions(),
                _ => throw new ArgumentException()
            };

            if (options is ChromeOptions chromeOpts)
                chromeOpts.AddArguments("--no-sandbox", "--disable-dev-shm-usage");

            if (options is EdgeOptions edgeOpts)
                edgeOpts.AddArguments("--no-sandbox", "--disable-dev-shm-usage");

            // CAMBIO: ToCapabilities() convierte DriverOptions a ICapabilities
            return new RemoteWebDriver(
                new Uri(TestConfiguration.GridUrl),
                options.ToCapabilities(),
                TimeSpan.FromMinutes(3)
            );
        }


    }
}
