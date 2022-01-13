using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Models
{
    public class Product
    {
        public int? inProductId { get; set; }
        public string stProductName { get; set; }
        public decimal? dcPrice { get; set; }
        public string stDescription { get; set; }
        public string stDiscount { get; set; }
        public int? inQuantity { get; set; }
        public bool flgOutOfStock { get; set; }
    }
}
