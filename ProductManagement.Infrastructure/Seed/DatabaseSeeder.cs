using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Infrastructure.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // CAMBIO: async para consistencia con el resto del flujo
            if (context.Products.Any())
                return;

            var products = new List<Product>
            {
                new Product
                {
                    Name        = "Notebook",
                    Description = "Gaming notebook",
                    Price       = 1500,
                    Stock       = 10,
                    IsActive    = true
                },
                new Product
                {
                    Name        = "Keyboard",
                    Description = "Mechanical keyboard",
                    Price       = 100,
                    Stock       = 20,
                    IsActive    = true
                }
            };

            context.Products.AddRange(products);

            // CAMBIO: async
            await context.SaveChangesAsync();
        }
    }
}
