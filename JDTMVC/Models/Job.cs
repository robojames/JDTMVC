using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JDTMVC.Models
{
    public class Job
    {
        
        public long Id { get; set; }

        [Required]
        [Display(Name = "Job Number")]
        [StringLength(160, MinimumLength = 5)]
        public string name { get; set; }
        
        [Display(Name = "Static Testing")]
        public bool Static_Testing { get; set; }

        [Display(Name = "Dynamic Testing")]
        public bool Dynamic_Testing { get; set; }
        
        [Required]
        [Display(Name="Project Manager")]
        public string PM { get; set; }

        public string Engineer { get; set; }

    }
}