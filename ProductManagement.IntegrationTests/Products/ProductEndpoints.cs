using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.IntegrationTests.Products
{
    public static class ProductEndpoints
    {

        public const string Base = "/api/products";


        public static string ById(int id)
        {
            return $"{Base}/{id}";
        }

    }
}
