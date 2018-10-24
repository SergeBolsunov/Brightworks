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
    public class MessagingsController : Controller
    {
        private BrightworksEntities db = new BrightworksEntities();

        // GET: Messagings
        public ActionResult Index()
        {
            var messagings = db.Messagings.Include(m => m.Employee);
            return View(messagings.ToList());
        }

        // GET: Messagings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messaging messaging = db.Messagings.Find(id);
            if (messaging == null)
            {
                return HttpNotFound();
            }
            return View(messaging);
        }

        // GET: Messagings/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Employees, "ID", "Title");
            return View();
        }

        // POST: Messagings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MID,Body,UserID,Usertime")] Messaging messaging)
        {
            if (ModelState.IsValid)
            {
                db.Messagings.Add(messaging);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Employees, "ID", "Title", messaging.UserID);
            return View(messaging);
        }

        // GET: Messagings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messaging messaging = db.Messagings.Find(id);
            if (messaging == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Employees, "ID", "Title", messaging.UserID);
            return View(messaging);
        }

        // POST: Messagings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MID,Body,UserID,Usertime")] Messaging messaging)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messaging).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Employees, "ID", "Title", messaging.UserID);
            return View(messaging);
        }

        // GET: Messagings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messaging messaging = db.Messagings.Find(id);
            if (messaging == null)
            {
                return HttpNotFound();
            }
            return View(messaging);
        }

        // POST: Messagings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Messaging messaging = db.Messagings.Find(id);
            db.Messagings.Remove(messaging);
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
