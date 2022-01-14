using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Models
{
    public class ResponseObject
    {
        public int? inProductId { get; set; }
        public string stProductName { get; set; }
        public decimal? dcPrice { get; set; }
        public string stDescription { get; set; }
        public decimal? dcDiscount { get; set; }
        public int? inQuantity { get; set; }
    }
}
