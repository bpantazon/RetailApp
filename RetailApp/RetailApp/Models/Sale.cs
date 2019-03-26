using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RetailApp.Models
{
    public class Sale
    {
        [Key, Column(Order = 0)]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employees { get; set; }

        [Key, Column(Order = 1)]
        public int InventoryId { get; set; }
        [ForeignKey("InventoryId")]
        public virtual Inventory Inventories { get; set; }
    }
}