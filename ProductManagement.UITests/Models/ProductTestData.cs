using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Models
{
    public class ProductTestData
    {

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }



    }
}
