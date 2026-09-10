using System;
using System.Collections.Generic;
using System.Text;

using ProductManagement.API.DTOs;

namespace ProductManagement.IntegrationTests.Fixtures
{
    public static class ProductTestData
    {
        public static CreateProductDTO CreateValidProduct()
        {
            return new CreateProductDTO
            {
                Name = "Keyboard Logitech",
                Description = "Mechanical keyboard",
                Price = 150,
                Stock = 10
            };
        }

        public static UpdateProductDTO CreateUpdateProduct()
        {
            return new UpdateProductDTO
            {
                Name = "Updated Keyboard",
                Description = "Updated description",
                Price = 200,
                Stock = 15,
                IsActive = true
            };
        }
    }


}
