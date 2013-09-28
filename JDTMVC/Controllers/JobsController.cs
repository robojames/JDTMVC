﻿using System;
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
        string[] Engineer_List = new string[3] { "", "Eric Poole", "Wills Houghland" };
        string[] Status_List = new string[5] { "", "Running", "On Hold", "MFG", "Done" };

        

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
            ViewBag.StatusList = new SelectList(Status_List);
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
     
    }
}
