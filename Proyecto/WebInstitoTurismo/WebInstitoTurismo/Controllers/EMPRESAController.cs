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
    public class EMPRESAController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();

        // GET: EMPRESA
        public ActionResult Index()
        {
            var eMPRESA = db.EMPRESA.Include(e => e.REGION).Include(e => e.TIPO_EMPRESA);
            return View(eMPRESA.ToList());
        }

        // GET: EMPRESA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA eMPRESA = db.EMPRESA.Find(id);
            if (eMPRESA == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESA);
        }

        // GET: EMPRESA/Create
        public ActionResult Create()
        {
            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE");
            ViewBag.ID_TIPO = new SelectList(db.TIPO_EMPRESA, "ID", "TIPO");
            return View();
        }

        // POST: EMPRESA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE,DIRECCION,TELEFONO,CORREO,ESTADO,APROBADO,ID_TIPO,ID_REGION,ADMINISTRADOR")] EMPRESA eMPRESA)
        {
            if (ModelState.IsValid)
            {
                db.EMPRESA.Add(eMPRESA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE", eMPRESA.ID_REGION);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_EMPRESA, "ID", "TIPO", eMPRESA.ID_TIPO);
            return View(eMPRESA);
        }

        // GET: EMPRESA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA eMPRESA = db.EMPRESA.Find(id);
            if (eMPRESA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE", eMPRESA.ID_REGION);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_EMPRESA, "ID", "TIPO", eMPRESA.ID_TIPO);
            return View(eMPRESA);
        }

        // POST: EMPRESA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,DIRECCION,TELEFONO,CORREO,ESTADO,APROBADO,ID_TIPO,ID_REGION,ADMINISTRADOR")] EMPRESA eMPRESA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPRESA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE", eMPRESA.ID_REGION);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_EMPRESA, "ID", "TIPO", eMPRESA.ID_TIPO);
            return View(eMPRESA);
        }

        // GET: EMPRESA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA eMPRESA = db.EMPRESA.Find(id);
            if (eMPRESA == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESA);
        }

        // POST: EMPRESA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPRESA eMPRESA = db.EMPRESA.Find(id);
            db.EMPRESA.Remove(eMPRESA);
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
