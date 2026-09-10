using OpenQA.Selenium;
using ProductManagement.UITests.Models;
using System.Globalization;

namespace ProductManagement.UITests.Pages
{
    public class ProductEditPage : BasePage
    {
        // Selectores
        private readonly By _editTitle = By.Id("edit-product-title");
        private readonly By _nameInput = By.Id("product-name");
        private readonly By _descriptionInput = By.Id("product-description");
        private readonly By _priceInput = By.Id("product-price");
        private readonly By _stockInput = By.Id("product-stock");
        private readonly By _saveButton = By.Id("btn-save-product");


        public ProductEditPage(IWebDriver driver) : base(driver) { }



        public override bool IsLoaded()
        {
            try
            {
                return Driver.FindElement(_editTitle).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }



        public void UpdateProduct(ProductTestData product)
        {
            UpdateName(product.Name);
            UpdateDescription(product.Description);
            UpdatePrice(product.Price);
            UpdateStock(product.Stock);

            ClickSave();
        }



        public void UpdateName(string name) => Write(_nameInput, name);

        public void UpdateDescription(string description) => Write(_descriptionInput, description);

        public void UpdatePrice(decimal price) => Write(_priceInput, price.ToString(CultureInfo.InvariantCulture));

        public void UpdateStock(int stock) => Write(_stockInput, stock.ToString());

        public void ClickSave() => Click(_saveButton);
    }


}
