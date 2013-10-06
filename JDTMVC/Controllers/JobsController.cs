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
        string[] PM_List = new string[3] { "", "John Murray", "Vinay Shaw" };
        string[] Engineer_List = new string[5] { "", "Eric Poole", "Wills Houghland", "James Armes", "Ed Ogodrony" };
        string[] Status_List = new string[12] { "", "Cancelled", "Closed", "Done", "Hold", "MFG", "Pre-Test", "Quarantine", "Receive", "Running", "Science Fair", "Wait Queue" };
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

        [HttpGet]
        public ActionResult Create()
        {

            ViewData["PMLIST"] = new SelectList(PM_List);
            ViewData["ENGINEERLIST"] = new SelectList(Engineer_List);
            ViewData["StatusList"] = new SelectList(Status_List);

            return View();
        }

        // 
        // Creates a brand new job to the DB
        //
        [HttpPost]
        public ActionResult Create(Models.Job newjob)
        {
            if (ModelState.IsValid)
            {
                // Save to the database
                var db = new JobsDataContext();

                newjob.curr_Revision = "A";

                db.Jobs.Add(newjob);

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

                Debug.WriteLine("Current Revision: " + job.curr_Revision);

                db.Jobs.Add(job);

                db.SaveChanges();
            }
                return RedirectToAction("Index");
        }

        public ActionResult Delete(Models.Job job)
        {
            var db = new JobsDataContext();

            db.Entry(job).State = System.Data.EntityState.Deleted;

            db.SaveChanges();

            return View("Index");
        }

        public ActionResult DueDate()
        {
            var db = new JobsDataContext();

            var done_jobs = (IEnumerable<Job>)db.Jobs.Where(job => job.Status == "Done").OrderByDescending(job => job.Engineer);

            return View(done_jobs);
        }
     
    }
}
