using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailApp.Models
{
    public class StoreProduct
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Brand")]
        public string BrandName { get; set; }

        [Display(Name = "Model")]
        public string ModelName { get; set; }

        public decimal Price { get; set; }

        public string SKU { get; set; }

        public string Category { get; set; }

        public int Count { get; set; }
    }
}