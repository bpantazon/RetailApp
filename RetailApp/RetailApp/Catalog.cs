using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace RetailApp.Models
{

    public class Catalog
    {
        public int Product_Id { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; }
        [Display(Name = "Model")]
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        [Display(Name = "Feature 1")]
        public string Feature1 { get; set; }
        [Display(Name = "Feature 2")]
        public string Feature2 { get; set; }
        public string Summary { get; set; }
    }

}