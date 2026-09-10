using OpenQA.Selenium;
using ProductManagement.UITests.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProductManagement.UITests.Pages
{
    public class ProductCreatePage : BasePage
    {
        // Selectores
        private readonly By _nameInput = By.Id("product-name");
        private readonly By _descriptionInput = By.Id("product-description");
        private readonly By _priceInput = By.Id("product-price");
        private readonly By _stockInput = By.Id("product-stock");
        private readonly By _activeCheckbox = By.Id("product-active");
        private readonly By _saveButton = By.Id("btn-save-product");


        public ProductCreatePage(IWebDriver driver) : base(driver) { }



        public override bool IsLoaded()
        {
            try
            {
                return Driver.FindElement(_saveButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }



        public void CreateProduct(ProductTestData product)
        {
            EnterName(product.Name);
            EnterDescription(product.Description);
            EnterPrice(product.Price);
            EnterStock(product.Stock);

            if (product.IsActive)
            {
                SetActive();
            }

            ClickSave();
        }



        public void EnterName(string name) => Write(_nameInput, name);

        public void EnterDescription(string description) => Write(_descriptionInput, description);

        public void EnterPrice(decimal price) =>  Write(_priceInput, price.ToString(CultureInfo.InvariantCulture));

        public void EnterStock(int stock) => Write(_stockInput, stock.ToString());


        public void SetActive()
        {
            var checkbox = Driver.FindElement(_activeCheckbox);

            if (!checkbox.Selected)
            {
                checkbox.Click();
            }
        }


        public void ClickSave() => Click(_saveButton);



    }


}
