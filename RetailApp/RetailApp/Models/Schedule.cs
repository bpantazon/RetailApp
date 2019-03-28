using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RetailApp.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

          [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }


        //Change to a dropdown of existing employees in a later iteration?
        [Display(Name = "Employee First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Employee Last Name")]
        public string LastName { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employees { get; set; }
    }
}