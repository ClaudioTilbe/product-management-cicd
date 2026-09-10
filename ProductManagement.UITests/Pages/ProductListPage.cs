using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Pages
{
    public class ProductListPage : BasePage
    {

        // Selectores
        private readonly By _productsTitle = By.Id("products-title");
        private readonly By _createButton = By.Id("btn-create-product");
        private readonly By _searchInput = By.Id("search-product-id");
        private readonly By _emptyMessage = By.Id("empty-products-message");


        public ProductListPage(IWebDriver driver) : base(driver) { }



        public override bool IsLoaded()
        {
            try
            {
                return Driver.FindElement(_productsTitle).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }



        //public void ClickCreateProduct() => Click(_createButton);

        //public void SearchProductById(int id) => Write(_searchInput, id.ToString());

        public bool ProductExists(int productId) => Driver.FindElements(By.Id($"product-row-{productId}")).Any();

        public void WaitUntilProductIsDeleted(int productId) => WaitUntil(driver => !ProductExists(productId));

        //public void ClickEditProduct(int productId) => Click(By.Id($"btn-edit-product-{productId}"));

        //public string GetEmptyMessage() => Driver.FindElement(_emptyMessage).Text;

        public void ClickDeleteProduct(int productId) => Click(By.Id($"btn-delete-product-{productId}"));

        public void AcceptDeleteConfirmation() => Driver.SwitchTo().Alert().Accept();

    }


}