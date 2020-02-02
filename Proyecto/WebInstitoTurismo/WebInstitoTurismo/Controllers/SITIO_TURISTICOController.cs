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
    public class SITIO_TURISTICOController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();

        // GET: SITIO_TURISTICO
        public ActionResult Index()
        {
            db.Database.ExecuteSqlCommand("DELETE FROM SITIO_TURISTICO WHERE ID NOT IN(SELECT ID_SITIO FROM FOTO_SITIO);");
            var sITIO_TURISTICO = db.SITIO_TURISTICO.Include(s => s.REGION);            
            return View(sITIO_TURISTICO.ToList());
        }

        // GET: SITIO_TURISTICO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SITIO_TURISTICO sITIO_TURISTICO = db.SITIO_TURISTICO.Find(id);
            if (sITIO_TURISTICO == null)
            {
                return HttpNotFound();
            }
            return View(sITIO_TURISTICO);
        }

        // GET: SITIO_TURISTICO/Create
        public ActionResult Create()
        {
            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE");
            return View();
        }

        // POST: SITIO_TURISTICO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOMBRE,DIRECCION,ID_REGION")] SITIO_TURISTICO sITIO_TURISTICO)
        {
            if (ModelState.IsValid)
            {
                db.SITIO_TURISTICO.Add(sITIO_TURISTICO);
                db.SaveChanges();

                return RedirectToAction("Create/" + sITIO_TURISTICO.ID, "FOTO_SITIO");
            }

            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE", sITIO_TURISTICO.ID_REGION);
            return View(sITIO_TURISTICO);
        }

        // GET: SITIO_TURISTICO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SITIO_TURISTICO sITIO_TURISTICO = db.SITIO_TURISTICO.Find(id);
            if (sITIO_TURISTICO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE", sITIO_TURISTICO.ID_REGION);
            return View(sITIO_TURISTICO);
        }

        // POST: SITIO_TURISTICO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,DIRECCION,ID_REGION")] SITIO_TURISTICO sITIO_TURISTICO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sITIO_TURISTICO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE", sITIO_TURISTICO.ID_REGION);
            return View(sITIO_TURISTICO);
        }

        // GET: SITIO_TURISTICO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SITIO_TURISTICO sITIO_TURISTICO = db.SITIO_TURISTICO.Find(id);
            if (sITIO_TURISTICO == null)
            {
                return HttpNotFound();
            }
            return View(sITIO_TURISTICO);
        }

        // POST: SITIO_TURISTICO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SITIO_TURISTICO sITIO_TURISTICO = db.SITIO_TURISTICO.Find(id);
                db.SITIO_TURISTICO.Remove(sITIO_TURISTICO);
                db.SaveChanges();
            }
            catch (DataException dex)
            {
                ModelState.AddModelError("No se puede borrar debido a sus relaciones", dex.Message);
                return RedirectToAction("Delete");
            }
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
