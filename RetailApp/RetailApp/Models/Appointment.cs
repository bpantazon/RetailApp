using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RetailApp.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Customer First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Customer Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Customer Contact Number")]
        public int PhoneNumber { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customers { get; set; }
    }
}