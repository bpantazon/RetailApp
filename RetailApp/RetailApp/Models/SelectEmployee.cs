using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailApp.Models
{
    public class SelectEmployee
    {
        public int EmployeeId { get; set; }
        public IEnumerable<Employee> EmployeeList { get; set; }
    }
}