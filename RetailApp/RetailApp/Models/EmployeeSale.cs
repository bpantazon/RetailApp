using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RetailApp.Models
{
    public class EmployeeSale
    {
        [Key]
        public int EmpSaleId { get; set; }

        public string Model { get; set; }

        [Display(Name = "Number Sold")]
        public int NumberSold { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}