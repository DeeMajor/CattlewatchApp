using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CattlewatchMVC.Models;

namespace CattlewatchMVC.Controllers
{
    public class LivestocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Livestocks
        public ActionResult Index()
        {
            return View(db.Livestocks.ToList());
        }

        // GET: Livestocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livestock livestock = db.Livestocks.Find(id);
            if (livestock == null)
            {
                return HttpNotFound();
            }
            return View(livestock);
        }

        // GET: Livestocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livestocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LivestockId,type")] Livestock livestock)
        {
            if (ModelState.IsValid)
            {
                db.Livestocks.Add(livestock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(livestock);
        }

        // GET: Livestocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livestock livestock = db.Livestocks.Find(id);
            if (livestock == null)
            {
                return HttpNotFound();
            }
            return View(livestock);
        }

        // POST: Livestocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LivestockId,type")] Livestock livestock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livestock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livestock);
        }

        // GET: Livestocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livestock livestock = db.Livestocks.Find(id);
            if (livestock == null)
            {
                return HttpNotFound();
            }
            return View(livestock);
        }

        // POST: Livestocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livestock livestock = db.Livestocks.Find(id);
            db.Livestocks.Remove(livestock);
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
