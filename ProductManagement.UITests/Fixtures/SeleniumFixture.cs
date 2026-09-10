using OpenQA.Selenium;
using ProductManagement.UITests.Configuration;
using ProductManagement.UITests.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Fixtures
{
    public class SeleniumFixture : IDisposable
    {

        public IWebDriver Driver { get; }

        public SeleniumFixture(BrowserType browser)
        {
            Driver = WebDriverFactory.Create(browser);
        }


        public void Dispose()
        {
            Driver.Quit();

            Driver.Dispose();
        }


    }
}
