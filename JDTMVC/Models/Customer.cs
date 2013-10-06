using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JDTMVC.Models
{
    public class Customer
    {
        public long Id { get; set; }

        [Display(Name="First Name:")]
        [Required(ErrorMessage="Please enter the first name of the customer.")]
        public string first_Name { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage="Please enter the last name of the customer.")]
        public string last_Name { get; set; }

        [Display(Name="Company Name:")]
        [Required(ErrorMessage="Please enter the company name.")]
        public string Company_Name { get; set; }
    }
}