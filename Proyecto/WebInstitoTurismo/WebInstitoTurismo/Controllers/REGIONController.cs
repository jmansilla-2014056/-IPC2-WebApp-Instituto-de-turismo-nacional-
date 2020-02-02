using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebInstitoTurismo.Models;

namespace WebInstitoTurismo.Controllers
{
    public class REGIONController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();

        // GET: REGION
        public ActionResult Index()
        {
            return View(db.REGION.ToList());
        }

        // GET: REGION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGION rEGION = db.REGION.Find(id);
            if (rEGION == null)
            {
                return HttpNotFound();
            }
            return View(rEGION);
        }

        // GET: REGION/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: REGION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE")] REGION rEGION)
        {
            if (ModelState.IsValid)
            {
                db.REGION.Add(rEGION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rEGION);
        }

        // GET: REGION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGION rEGION = db.REGION.Find(id);
            if (rEGION == null)
            {
                return HttpNotFound();
            }
            return View(rEGION);
        }

        // POST: REGION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE")] REGION rEGION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEGION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rEGION);
        }

        // GET: REGION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGION rEGION = db.REGION.Find(id);
            if (rEGION == null)
            {
                return HttpNotFound();
            }
            return View(rEGION);
        }

        // POST: REGION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REGION rEGION = db.REGION.Find(id);
            db.REGION.Remove(rEGION);
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
