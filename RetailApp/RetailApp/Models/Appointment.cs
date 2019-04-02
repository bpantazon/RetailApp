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

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Contact Number")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}