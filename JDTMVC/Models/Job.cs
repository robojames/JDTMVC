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

        [Display(Name="Report or Memo")]
        public string Report_Or_Memo { get; set; }
        
        // Dates for the Job Model
        [Display(Name = "PO Date")]
        [DataType(DataType.Date)]
        public DateTime? PO_Date { get; set; }

        [Display(Name="Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name="Folder Build Date")]
        [DataType(DataType.Date)]
        public DateTime? FolderBuild { get; set; }

        [Display(Name="Folder Check Date")]
        [DataType(DataType.Date)]
        public DateTime? FolderCheck { get; set; }

        [Display(Name = "Test Start Date")]
        [DataType(DataType.Date)]
        public DateTime? TestStart { get; set; }

        [Display(Name="Test End Date")]
        [DataType(DataType.Date)]
        public DateTime? TestEnd { get; set; }
        
        [Display(Name="Report Write Date")]
        [DataType(DataType.Date)]
        public DateTime? ReportWritten { get; set; }
        
        [Display(Name="Report Initial Check Date")]
        [DataType(DataType.Date)]
        public DateTime? Report_Initial_Check { get; set; }

        [Display(Name = ("Report Final Check Date"))]
        [DataType(DataType.Date)]
        public DateTime? Report_Final_Check { get; set; }
                
        [Display(Name = ("Invoice Date"))]
        [DataType(DataType.Date)]
        public DateTime? Invoiced { get; set; }

        [Display(Name=("System Name"))]
        public string SystemName { get; set; }

        [Display(Name=("Device Type"))]
        public string DeviceType { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string Comments { get; set; }
        
        [Display(Name = "Job Status")]
        [Required(ErrorMessage="Please enter the current status of the job")]
        public string Status { get; set; }
        
        public string Engineer { get; set; }

        [Display(Name="Rev.")]
        public string curr_Revision { get; set; }

        [Display(Name="Customer Name")]
        public string customer_Name { get; set; }

        [Display(Name="Customer Company")]
        public string customer_Company { get; set; }

        public string Calipers { get; set; }

        [Display(Name="Caliper Date")]
        public DateTime? Caliper_Date { get; set; }

        public string Micrometer { get; set; }

        [Display(Name = "Micrometer Date")]
        public DateTime? Micrometer_Date { get; set; }

        public string Scale { get; set; }

        [Display(Name = "Scale Date")]
        public DateTime? Scale_Date { get; set; }

        [Display(Name="Torque Wrench")]
        public string Torque_Wrench { get; set; }

        [Display(Name = "Torque Wrench Date")]
        public DateTime? Torque_Wrench_Date { get; set; }

        public string Protractor { get; set; }

        [Display(Name = "Protractor Date")]
        public DateTime? Protractor_Date { get; set; }

        [Display(Name="Load Verify (force):")]
        public string LV_Force { get; set; }

        [Display(Name = "Load Verify Date")]
        public DateTime? LV_Force_Date { get; set; }

        [Display(Name="Load Verify (torque):")]
        public string LV_Torque { get; set; }

        [Display(Name = "Load Verify Date")]
        public DateTime? LV_Torque_Date { get; set; } 

    }
}