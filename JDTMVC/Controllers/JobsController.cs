using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JDTMVC.Models;
using System.Diagnostics;
using System.Data.Entity.Validation;


namespace JDTMVC.Controllers
{
    public class JobsController : Controller
    {
        #region List Constants for Equipment and Personnel
        string[] PM_List = new string[3] { "", "John Murray", "Vinay Shaw" };
        
        string[] Engineer_List = new string[6] { "", "Eric Poole", "Wills Houghland", "James Armes", "Ed Ogodrony", "Bill Nye" };
        
        string[] Status_List = new string[12] { "", "Cancelled", "Closed", "Done", "Hold", "MFG", "Pre-Test", "Quarantine", "Receive", "Running", "Science Fair", "Wait Queue" };
        
        string[] Caliper_List = new string[25] { "", "CP002", "CP002, CP003, CP006, CP008", "CP002, CP004", "CP002, CP003", "CP002, CP005", "CP002, CP006", "CP002, CP008",
        "CP003", "CP003, CP005", "CP003, CP004", "CP003, CP004, CP005, CP008", "CP003, CPOO4, CPOO8", "CP003, CP006", "CP003, CP008", "CP004", "CP004, CP005",
        "CP005", "CP005, CP006", "CP005, CP008", "CP005, CP002", "CP005, CP003", "CP006", "CP006, CP008", "CP008"};

        string[] Micrometer_List = new string[3] { "", "DM001", "F125E" };
        
        string[] Scale_List = new string[19] { "", "DB001", "DB001, DB002", "DB001/SC07", "DB002", "DB002/DB001", "SC01", "SC02", "SC02, DB001", "SC02, DB001, DB002", "SC03", "SC04", "SC05", "SC06", "SC06, SC10",
        "SC07", "SC07, SC10", "SC08", "SC09"};
        
        string[] TW_List = new string[19] { "", "TW002", "TW003", "TW003, TW004", "TW003, TW005", "TW003, TW007", "TW004", "TW004, TW002", "TW004, TW005", "TW004, TW006", "TW005, TW007", "TW006", "TW006, TW003",
        "TW006, TW007", "TW006, TW009", "TW007", "TW008", "TW008, TW009", "TW009"};
        
        string[] Protractor_List = new string[2] { "", "DP001" };
        
        string[] LVF_List = new string[6] { "", "Deadweights (Tension)", "FG001", "FG001, FG002", "FG002", "Shunt Ref" };
        
        string[] LVT_List = new string[6] { "", "Shunt Ref", "TW002", "TW004", "TW005", "TW007" };

        string[] DeviceTypes = new string[] {
            "", 
            "ACP System", 
            "ALP System", 
            "Ankle Implant", 
            "Bone Screw", 
            "Clavicle", 
            "Coating", 
            "Connector", 
            "Dental Implant", 
            "Drill", 
            "Elbow Implant", 
            "ESFD", 
            "Foot Implant", 
            "Hip Implant", 
            "Hook", 
            "IBFD Cervical", 
            "IBFD Lumbar", 
            "IM Device", 
            "Instruments",
            "ISP Lumbar",
            "ISS Lumbar",
            "Knee Implant",
            "Lumbar SA",
            "OCT System",
            "PDS System",
            "Plate - Bone",
            "Plate - Laminoplasty",
            "PLIF",
            "Protocol Only",
            "Prototyping",
            "PSS Cervical",
            "PSS Component",
            "PSS Lumbar",
            "Regulatory",
            "Rods",
            "RSP",
            "SA Cervicle",
            "SA Lumbar",
            "Screw - Facet",
            "Screw - Non-Facet",
            "Shoulder Implant",
            "Staple",
            "Sterilization",
            "TDR Cervical",
            "TDR Lumbar",
            "Test Blocks Only",
            "Thumb Implant",
            "Tibial Tray",
            "Toe",
            "VBR",
            "Wrist Implant" 
       };

        string[] ReportTypes = new string[3] { "", "Report", "Memo" };

        #endregion

        // Main entry point for the user
        public ActionResult Index()
        {
            var db = new JobsDataContext();

            var jobs = db.Jobs.ToArray();

            
                var model = jobs.Select(t => new SelectListItem
                {
                    Text = t.name.ToString(),
                    Value = t.name.ToString()
                }).ToList();

                model.Insert(0, new SelectListItem { Value = "-1", Text = "Select a Job" });

                ViewData["TableSelect"] = model;

            return View();

        }

        // Finds job based on passed in name (Job number)
        public ActionResult FindJob(string name)
        {
            var db = new JobsDataContext();

            if (name != "")
            {
                var job = (IEnumerable<Models.Job>)db.Jobs.Where(t => t.name == name);

                if (job == null)
                {
                    Index();

                    return View("Index");
                }
                else
                {
                    return View(job);
                }
            }
            else
            {
                var job = (IEnumerable<Models.Job>)db.Jobs;
                
                if (job == null)
                {
                    Index();

                    return View("Index");
                }
                else
                {
                    return View(job);
                }
            }

            
        }

        // Action to redirect to the edit page per job id (this is linked to the PK in the DB, NOT the job number)
        [HttpGet]
        public ActionResult Edit(long id)
        {
            //ViewData["PMLIST"] = new SelectList(PM_List);
            //ViewData["ENGINEERLIST"] = new SelectList(Engineer_List);
            //ViewData["StatusList"] = new SelectList(Status_List);

            ViewBag.PMList = new SelectList(PM_List);
            ViewBag.StatusList = new SelectList(Status_List);
            ViewBag.EngineerList = new SelectList(Engineer_List);
            ViewBag.DeviceList = new SelectList(DeviceTypes);
            ViewBag.ReportList = new SelectList(ReportTypes);

            ViewBag.CaliperList = new SelectList(Caliper_List);
            ViewBag.MicrometerList = new SelectList(Micrometer_List);
            ViewBag.ScaleList = new SelectList(Scale_List);
            ViewBag.TWList = new SelectList(TW_List);
            ViewBag.ProtractorList = new SelectList(Protractor_List);
            ViewBag.LVForceList = new SelectList(LVF_List);
            ViewBag.LVTorqueList = new SelectList(LVT_List);


            var db_Customer = new CustomersDataContext();

            var customers = (IEnumerable<Models.Customer>)db_Customer.Customers;

            List<string> customer_List = new List<string>();
            List<string> customer_Companies = new List<string>();

            customer_List.Add("");
            customer_Companies.Add("");

            foreach (Customer customer in customers)
            {
                customer_List.Add(customer.first_Name + " " + customer.last_Name);
                customer_Companies.Add(customer.Company_Name.ToString());
            }

            ViewBag.CustomerList = new SelectList(customer_List);
            ViewBag.CompanyList = new SelectList(customer_Companies);

            var db = new JobsDataContext();
            
            var job = db.Jobs.Find(id);

            return View(job);
        }

        // Action to post edited values to the database, job passed in as object
        [HttpPost]
        public ActionResult Edit(Models.Job editedjob)
        {
            if (ModelState.IsValid)
            {
                // Save to the database
                var db = new JobsDataContext();
                db.Entry(editedjob).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return Edit(editedjob.Id);
            }
        }

        // Redirects to the create page for a new job
        [HttpGet]
        public ActionResult Create()
        {

            ViewData["PMLIST"] = new SelectList(PM_List);
            ViewData["ENGINEERLIST"] = new SelectList(Engineer_List);
            ViewData["StatusList"] = new SelectList(Status_List);

            return View();
        }

        
        // Creates a brand new job in the DB
        [HttpPost]
        public ActionResult Create(Models.Job newjob)
        {
            if (ModelState.IsValid)
            {
                // Save to the database
                var db = new JobsDataContext();

                // Sets the current revision to A, as it should be for any new job created
                newjob.curr_Revision = "A";

                db.Jobs.Add(newjob);

                // Checks to see if there are any validation errors returned by the JobsDataContext comparitor
                if (db.GetValidationErrors().Count() > 0)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return Create();
                        
        }

        public ActionResult AddRevision(Models.Job job)
        {
            if (ModelState.IsValid)
            {
                var db = new JobsDataContext();

                char current_Revision = char.Parse(job.curr_Revision);

                current_Revision++;

                job.curr_Revision = current_Revision.ToString();

                #if DEBUG
                    Debug.WriteLine("Current Revision: " + job.curr_Revision);
                #endif
                db.Jobs.Add(job);

                db.SaveChanges();
            }
                return RedirectToAction("Index");
        }

        // Need to create a view for this...currently the only prompt is given by jQuery.
        public ActionResult Delete(Models.Job job)
        {
            var db = new JobsDataContext();

            db.Entry(job).State = System.Data.EntityState.Deleted;

            db.SaveChanges();

            return View("Index");
        }

        // Grabs data and returns view for the due date summary
        public ActionResult DueDate()
        {
            var db = new JobsDataContext();

            var done_jobs = (IEnumerable<Job>)db.Jobs.Where(job => job.Status == "Done").OrderByDescending(job => job.Engineer);

            return View(done_jobs);
        }

        public ActionResult JobDistro()
        {
            var db = new JobsDataContext();

            var JobCount = new List<int>();

            var Engineers = Engineer_List.Skip(1);

            foreach (string Engineer in Engineer_List.Skip(1))
            {
                var jobsbyEng = db.Jobs.Where(job => job.Engineer == Engineer).Count();

                JobCount.Add(jobsbyEng);
            }

            ViewBag.JobCounts = JobCount;
            ViewBag.Engineers = Engineers;

            ViewData["Engineers"] = Engineers.ToArray();
            
            return View();
        }
     
    }
}
