using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrightWorks.Models;
using Microsoft.AspNet.Identity;

namespace BrightWorks.Controllers
{   
    [Authorize]
    public class EmployeeCheckInsController : Controller
    {
        private BrightworksEntities db = new BrightworksEntities();

        // GET: EmployeeCheckIns
        public ActionResult Index()
        {
            var employeeCheckIns = db.EmployeeCheckIns.Include(e => e.Employee).Where(x=>x.Usertime > DateTime.Today);
            var sortcheckss = employeeCheckIns.AsEnumerable().OrderBy(e => e.Employee.FullName);
            return View(sortcheckss.ToList());
        }

        // GET: EmployeeCheckIns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeCheckIn employeeCheckIn = db.EmployeeCheckIns.Find(id);
            if (employeeCheckIn == null)
            {
                return HttpNotFound();
            }
            return View(employeeCheckIn);
        }

        // GET: EmployeeCheckIns/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: EmployeeCheckIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Latitude,Longitude,EID")] EmployeeCheckIn employeeCheckIn)
        {
            if (ModelState.IsValid)
            {
                employeeCheckIn.UserID = User.Identity.GetUserId();
                employeeCheckIn.Usertime = DateTime.Now;
                db.EmployeeCheckIns.Add(employeeCheckIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(employeeCheckIn);
        }

        // GET: EmployeeCheckIns/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EmployeeCheckIn employeeCheckIn = db.EmployeeCheckIns.Find(id);
        //    if (employeeCheckIn == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserID = new SelectList(db.Employees, "ID", "Title", employeeCheckIn.UserID);
        //    return View(employeeCheckIn);
        //}

        // POST: EmployeeCheckIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "UserID,Usertime,Position,EID")] EmployeeCheckIn employeeCheckIn)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(employeeCheckIn).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserID = new SelectList(db.Employees, "ID", "Title", employeeCheckIn.UserID);
        //    return View(employeeCheckIn);
        //}

        // GET: EmployeeCheckIns/Delete/5
        //  public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EmployeeCheckIn employeeCheckIn = db.EmployeeCheckIns.Find(id);
        //    if (employeeCheckIn == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employeeCheckIn);
        //}

        // POST: EmployeeCheckIns/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    EmployeeCheckIn employeeCheckIn = db.EmployeeCheckIns.Find(id);
        //    db.EmployeeCheckIns.Remove(employeeCheckIn);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
