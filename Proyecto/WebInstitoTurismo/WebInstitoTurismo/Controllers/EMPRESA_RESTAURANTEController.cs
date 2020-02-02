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
    public class EMPRESA_RESTAURANTEController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();

        // GET: EMPRESA_RESTAURANTE
        public ActionResult Index()
        {
            var eMPRESA_RESTAURANTE = db.EMPRESA_RESTAURANTE.Include(e => e.EMPRESA);
            return View(eMPRESA_RESTAURANTE.ToList());
        }

        // GET: EMPRESA_RESTAURANTE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA_RESTAURANTE eMPRESA_RESTAURANTE = db.EMPRESA_RESTAURANTE.Find(id);
            if (eMPRESA_RESTAURANTE == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESA_RESTAURANTE);
        }

        // GET: EMPRESA_RESTAURANTE/Create
        public ActionResult Create()
        {
            ViewBag.EMPRESA_ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE");
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE");
            ViewBag.Especialidades = db.ESPECIALIDAD.ToList();
            return View();
        }

        // POST: EMPRESA_RESTAURANTE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HORA_I,HORA_F,ID_EMPRESA,EMPRESA")] EMPRESA_RESTAURANTE eMPRESA_RESTAURANTE, int[] especials = null)
        {
            eMPRESA_RESTAURANTE.EMPRESA.ID_TIPO = 2;
            eMPRESA_RESTAURANTE.EMPRESA.ESTADO = false;
            eMPRESA_RESTAURANTE.EMPRESA.APROBADO = false;
            if (ModelState.IsValid)
            {
                db.EMPRESA.Add(eMPRESA_RESTAURANTE.EMPRESA);
                db.EMPRESA_RESTAURANTE.Add(eMPRESA_RESTAURANTE);
                db.SaveChanges();
                foreach(var c in especials)
                {
                    CATALOGO_RESTAURANTE catologoRestauarante = new CATALOGO_RESTAURANTE();
                    catologoRestauarante.VALIDACION = false;
                    catologoRestauarante.ID_RESTAURANTE = eMPRESA_RESTAURANTE.ID_EMPRESA;
                    catologoRestauarante.ID_ESPECIALIDAD = c;
                    db.CATALOGO_RESTAURANTE.Add(catologoRestauarante);
                    db.SaveChanges();
                }
               
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE");
            ViewBag.Especialidades = db.ESPECIALIDAD.ToList();
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", eMPRESA_RESTAURANTE.ID_EMPRESA);
            return View(eMPRESA_RESTAURANTE);
        }

        // GET: EMPRESA_RESTAURANTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA_RESTAURANTE eMPRESA_RESTAURANTE = db.EMPRESA_RESTAURANTE.Find(id);
            if (eMPRESA_RESTAURANTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", eMPRESA_RESTAURANTE.ID_EMPRESA);
            return View(eMPRESA_RESTAURANTE);
        }

        // POST: EMPRESA_RESTAURANTE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HORA_I,HORA_F,ID_EMPRESA")] EMPRESA_RESTAURANTE eMPRESA_RESTAURANTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPRESA_RESTAURANTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", eMPRESA_RESTAURANTE.ID_EMPRESA);
            return View(eMPRESA_RESTAURANTE);
        }

        // GET: EMPRESA_RESTAURANTE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA_RESTAURANTE eMPRESA_RESTAURANTE = db.EMPRESA_RESTAURANTE.Find(id);
            if (eMPRESA_RESTAURANTE == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESA_RESTAURANTE);
        }

        // POST: EMPRESA_RESTAURANTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPRESA_RESTAURANTE eMPRESA_RESTAURANTE = db.EMPRESA_RESTAURANTE.Find(id);
            db.EMPRESA_RESTAURANTE.Remove(eMPRESA_RESTAURANTE);
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
