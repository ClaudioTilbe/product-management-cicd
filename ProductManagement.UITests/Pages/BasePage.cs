using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace ProductManagement.UITests.Pages
{
    public abstract class BasePage
    {

        // readonly — ninguna subclase puede reasignarlo accidentalmente
        protected readonly IWebDriver Driver;

        protected virtual TimeSpan DefaultTimeout => TimeSpan.FromSeconds(10);


        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }



        protected void Click(By locator)
        {
            Driver.FindElement(locator).Click();
        }



        protected void Write(By locator, string value)
        {
            var element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(value);
        }



        protected void WaitForUrlContains(string text)
        {
            var wait = new WebDriverWait(Driver, DefaultTimeout);
            wait.Until(driver => driver.Url.Contains(text));
        }



        protected void WaitUntil(Func<IWebDriver, bool> condition)
        {
            var wait = new WebDriverWait(Driver, DefaultTimeout);
            wait.Until(condition);
        }



        // Patrón estándar para verificar que la página cargó
        // cada page sobreescribe los locators que debe esperar
        public virtual bool IsLoaded() => true;
    }



}
