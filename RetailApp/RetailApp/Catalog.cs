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
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
        public string Summary { get; set; }
    }

}