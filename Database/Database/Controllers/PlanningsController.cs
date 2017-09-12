using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database;

namespace Database.Controllers
{
    public class PlanningsController : Controller
    {
        private DatabaseBackendEntities db = new DatabaseBackendEntities();

        // GET: Plannings
        public ActionResult Index()
        {
            var plannings = db.Plannings.Include(p => p.Subject).Include(p => p.User);
            return View(plannings.ToList());
        }

        // GET: Plannings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = db.Plannings.Find(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            return View(planning);
        }

        // GET: Plannings/Create
        public ActionResult Create()
        {
            ViewBag.Course_Code = new SelectList(db.Subjects, "Course_Code", "Course_Name");
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "First_Name");
            return View();
        }

        // POST: Plannings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlanID,User_ID,Course_Code,Semester,Grade")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                db.Plannings.Add(planning);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course_Code = new SelectList(db.Subjects, "Course_Code", "Course_Name", planning.Course_Code);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "First_Name", planning.User_ID);
            return View(planning);
        }

        // GET: Plannings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = db.Plannings.Find(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course_Code = new SelectList(db.Subjects, "Course_Code", "Course_Name", planning.Course_Code);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "First_Name", planning.User_ID);
            return View(planning);
        }

        // POST: Plannings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlanID,User_ID,Course_Code,Semester,Grade")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planning).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course_Code = new SelectList(db.Subjects, "Course_Code", "Course_Name", planning.Course_Code);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "First_Name", planning.User_ID);
            return View(planning);
        }

        // GET: Plannings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = db.Plannings.Find(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            return View(planning);
        }

        // POST: Plannings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planning planning = db.Plannings.Find(id);
            db.Plannings.Remove(planning);
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
