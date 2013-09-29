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
        
        // Dates for the Job Model
        [Display(Name = "PO Date")]
        [DataType(DataType.Date)]
        public DateTime? PO_Date { get; set; }

        [Display(Name="Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        public DateTime? FolderBuild { get; set; }
        public DateTime? FolderCheck { get; set; }

        [Display(Name = "Test Start Date")]
        public DateTime? TestStart { get; set; }

        [Display(Name="Test End Date")]
        public DateTime? TestEnd { get; set; }
        
        [Display(Name="Report Write Date")]
        public DateTime? ReportWritten { get; set; }
        
        [Display(Name="Report Initial Check Date")]
        public DateTime? Report_Initial_Check { get; set; }

        [Display(Name = ("Report Final Check Date"))]
        public DateTime? Report_Final_Check { get; set; }
                
        [Display(Name = ("Invoice Date"))]
        public DateTime? Invoiced { get; set; }

        [Display(Name=("System Name"))]
        public string SystemName { get; set; }

        public string Comments { get; set; }
        
        [Display(Name = "Job Status")]
        [Required(ErrorMessage="Please enter the current status of the job")]
        public string Status { get; set; }
        
        public string Engineer { get; set; }

        [Display(Name="Rev.")]
        public string curr_Revision { get; set; }

    }
}