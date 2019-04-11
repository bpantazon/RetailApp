using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailApp.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        public string SKU { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public string Category { get; set; }

        public int Sold { get; set; }

    }
}