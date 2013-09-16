using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JDTMVC.Models;
using System.Diagnostics;

namespace JDTMVC.Controllers
{
    public class JobsController : Controller
    {
        string[] PM_List = new string[2] { "John Murray", "Vinay Shaw" };
        string[] Engineer_List = new string[2] { "Eric Poole", "Wills Houghland" };

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

        public ActionResult FindJob(string name)
        {

            ViewData["PMLIST"] = new SelectList(PM_List);
            ViewData["ENGINEERLIST"] = new SelectList(Engineer_List);

            var db = new JobsDataContext();


            var job = (Models.Job)db.Jobs.Where(t => t.name == name).FirstOrDefault();

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

        [HttpGet]
        public ActionResult Edit(long id)
        {
            ViewBag.PMList = new SelectList(PM_List);
            
            ViewBag.EngineerList = new SelectList(Engineer_List);

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

            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Job newjob)
        {
            if (ModelState.IsValid)
            {
                // Save to the database
                var db = new JobsDataContext();
                db.Jobs.Add(newjob);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return Create();
                        
        }
    }
}
