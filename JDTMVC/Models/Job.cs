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

        
        [Required(ErrorMessage="Please enter a job number")]
        [Display(Name = "Job Number")]
        [StringLength(160, MinimumLength = 5)]
        public string name { get; set; }
        
        [Display(Name = "Static Testing")]
        public bool Static_Testing { get; set; }

        [Display(Name = "Dynamic Testing")]
        public bool Dynamic_Testing { get; set; }
        
        [Required(ErrorMessage="Please select a PM")]
        [Display(Name="Project Manager")]
        public string PM { get; set; }
                
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:dd-MMM-yy}")]
        [Display(Name = "PO Date")]
        [DataType(DataType.Date)]
        public DateTime PO_Date { get; set; }

        [Display(Name = "Job Status")]
        [Required(ErrorMessage="Please enter the current status of the job")]
        public string Status { get; set; }
        

        public string Engineer { get; set; }

    }
}