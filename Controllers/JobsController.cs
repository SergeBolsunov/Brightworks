using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrightWorks.Models;

namespace BrightWorks.Controllers
{   
    //new changes after emails
    [Authorize]
    public class JobsController : Controller
    {
        private BrightworksEntities db = new BrightworksEntities();

        // GET: Jobs
        
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Foreman);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Job job = db.Jobs.Find(id);
        //    if (job == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(job);
        //}
        //tedttkjggkkgkg
        // GET: Jobs/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.ForemanUserID = new SelectList(db.Employees, "ID", "FullName");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "JobID,Descrip,Location,ForemanUserID")] Job job)
        {
            if (ModelState.IsValid)
            {
                var test = db.Employees.First(x => x.ID.Equals(job.ForemanUserID));
                EmailUtility.sendMail(test.Email, "New job assigned!!: "+job.Descrip+" at "+job.Location);
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.ForemanUserID = new SelectList(db.Employees, "ID", "FullName", job.ForemanUserID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForemanUserID = new SelectList(db.Employees, "ID", "FullName", job.ForemanUserID);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit([Bind(Include = "JobID,Descrip,Location,ForemanUserID")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ForemanUserID = new SelectList(db.Employees, "ID", "FullName", job.ForemanUserID);
            return View(job);
        }

        // GET: Jobs/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
