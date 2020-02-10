using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataLibrary.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public List<ProductPhoto> ProductPhotos { get; set; }
        public Product()
        {
            ProductPhotos = new List<ProductPhoto>();
        }

    }
}
