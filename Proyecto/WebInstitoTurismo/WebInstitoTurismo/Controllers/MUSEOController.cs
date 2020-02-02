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
    public class MUSEOController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();

        // GET: MUSEO
        public ActionResult Index()
        {
            var mUSEO = db.MUSEO.Include(m => m.EMPRESA);
            return View(mUSEO.ToList());
        }

        // GET: MUSEO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSEO mUSEO = db.MUSEO.Find(id);
            if (mUSEO == null)
            {
                return HttpNotFound();
            }
            return View(mUSEO);
        }

        // GET: MUSEO/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE");
            ViewBag.EMPRESA_ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE");
            return View();
        }

        // POST: MUSEO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HORA_INICIO,HORA_FIN,TARIFA,EMPRESA")] MUSEO mUSEO)
        {
            if (ModelState.IsValid)
            {
                mUSEO.EMPRESA.ESTADO = false;
                mUSEO.EMPRESA.APROBADO = false;
                mUSEO.EMPRESA.ID_TIPO = 1;
                db.EMPRESA.Add(mUSEO.EMPRESA);
                mUSEO.ID_EMPRESA = mUSEO.EMPRESA.ID;
                db.MUSEO.Add(mUSEO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", mUSEO.ID_EMPRESA);
            ViewBag.EMPRESA_ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE", mUSEO.EMPRESA.ID_REGION);
            return View(mUSEO);

        }

        // GET: MUSEO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSEO mUSEO = db.MUSEO.Find(id);
            if (mUSEO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", mUSEO.ID_EMPRESA);
            return View(mUSEO);
        }

        // POST: MUSEO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HORA_INICIO,HORA_FIN,TARIFA,ID_EMPRESA")] MUSEO mUSEO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUSEO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", mUSEO.ID_EMPRESA);
            return View(mUSEO);
        }

        // GET: MUSEO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSEO mUSEO = db.MUSEO.Find(id);
            if (mUSEO == null)
            {
                return HttpNotFound();
            }
            return View(mUSEO);
        }

        // POST: MUSEO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MUSEO mUSEO = db.MUSEO.Find(id);
            db.MUSEO.Remove(mUSEO);
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
