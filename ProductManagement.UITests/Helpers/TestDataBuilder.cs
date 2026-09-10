using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;
using ProductManagement.UITests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Helpers
{
    public class TestDataBuilder
    {

        private readonly ApplicationDbContext _context;


        public TestDataBuilder(ApplicationDbContext context)
        {
            _context = context;
        }



        public int CreateProduct(ProductTestData data)
        {
            var product = new Product
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                Stock = data.Stock,
                IsActive = data.IsActive
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return product.Id;
        }
    }



}
