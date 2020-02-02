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
    public class SERVICIOController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();

        // GET: SERVICIO
        public ActionResult Index()
        {
            return View(db.SERVICIO.ToList());
        }

        // GET: SERVICIO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVICIO sERVICIO = db.SERVICIO.Find(id);
            if (sERVICIO == null)
            {
                return HttpNotFound();
            }
            return View(sERVICIO);
        }

        // GET: SERVICIO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SERVICIO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DESCRIPCION")] SERVICIO sERVICIO)
        {
            if (ModelState.IsValid)
            {
                db.SERVICIO.Add(sERVICIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sERVICIO);
        }

        // GET: SERVICIO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVICIO sERVICIO = db.SERVICIO.Find(id);
            if (sERVICIO == null)
            {
                return HttpNotFound();
            }
            return View(sERVICIO);
        }

        // POST: SERVICIO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DESCRIPCION")] SERVICIO sERVICIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sERVICIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sERVICIO);
        }

        // GET: SERVICIO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVICIO sERVICIO = db.SERVICIO.Find(id);
            if (sERVICIO == null)
            {
                return HttpNotFound();
            }
            return View(sERVICIO);
        }

        // POST: SERVICIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SERVICIO sERVICIO = db.SERVICIO.Find(id);
            db.SERVICIO.Remove(sERVICIO);
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
