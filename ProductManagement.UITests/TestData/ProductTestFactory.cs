using ProductManagement.UITests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.TestData
{
    public static class ProductTestFactory
    {


        public static ProductTestData CreateValidProduct() => new ProductTestData
        {
            Name = "Mouse Logitech",
            Description = "Wireless mouse",
            Price = 50,
            Stock = 10,
            IsActive = true
        };

        public static ProductTestData CreateUpdatedProduct() => new ProductTestData
        {
            Name = "Updated Keyboard",
            Description = "Mechanical updated",
            Price = 200,
            Stock = 20,
            IsActive = true
        };





    }
}
