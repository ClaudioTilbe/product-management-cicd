using ProductManagement.API.DTOs;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UnitTests.Fixtures
{
    public static class ProductFixture
    {

        public static Product Create()
        {
            return new Product
            {
                Id = 1,
                Name = "Notebook Dell",
                Price = 1200,
                Stock = 10,
                IsActive = true
            };
        }


        public static CreateProductDTO CreateValidCreateDto()
        {
            return new CreateProductDTO
            {
                Name = "Mouse Logitech",
                Description = "Wireless",
                Price = 50,
                Stock = 20
            };
        }



        public static CreateProductDTO CreateInvalidPriceDto()
        {
            return new CreateProductDTO
            {
                Name = "Test",
                Description = "Invalid price",
                Price = 0,
                Stock = 10
            };
        }



        public static CreateProductDTO CreateInvalidStockDto()
        {
            return new CreateProductDTO
            {
                Name = "Test",
                Description = "Invalid stock",
                Price = 100,
                Stock = -1
            };
        }


        public static List<Product> CreateList()
        {

            return new List<Product>
            {

                new Product
                {
                    Id = 1,
                    Name = "Notebook",
                    Price = 1200,
                    Stock = 10,
                    IsActive = true
                },


                new Product
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 50,
                    Stock = 20,
                    IsActive = true
                }

            };

        }



        public static Product CreateForUpdate()
        {

            return new Product
            {
                Id = 1,
                Name = "Old Name",
                Description = "Old Description",
                Price = 100,
                Stock = 5,
                IsActive = true
            };

        }



        public static UpdateProductDTO CreateUpdateDto()
        {

            return new UpdateProductDTO
            {
                Name = "New Name",
                Description = "Updated",
                Price = 200,
                Stock = 10,
                IsActive = true
            };

        }




    }
}
