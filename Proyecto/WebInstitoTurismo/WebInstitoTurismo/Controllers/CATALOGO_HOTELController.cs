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
    public class CATALOGO_HOTELController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();

        // GET: CATALOGO_HOTEL
        public ActionResult Index()
        {
            var cATALOGO_HOTEL = db.CATALOGO_HOTEL.Include(c => c.EMPRESA).Include(c => c.SERVICIO);
            return View(cATALOGO_HOTEL.ToList());
        }

        // GET: CATALOGO_HOTEL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATALOGO_HOTEL cATALOGO_HOTEL = db.CATALOGO_HOTEL.Find(id);
            if (cATALOGO_HOTEL == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_HOTEL);
        }

        // GET: CATALOGO_HOTEL/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE");
            ViewBag.EMPRESA_ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE");
            ViewBag.Servicios = db.SERVICIO.ToList();
            return View();
        }

        // POST: CATALOGO_HOTEL/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VALIDACION,ID_SERVICIO,ID_EMPRESA,EMPRESA")] CATALOGO_HOTEL cATALOGO_HOTEL, int[] services = null)
        {

            if (ModelState.IsValid)
            {
                cATALOGO_HOTEL.VALIDACION = false;
                cATALOGO_HOTEL.EMPRESA.ESTADO = false;
                cATALOGO_HOTEL.EMPRESA.APROBADO = false;
                cATALOGO_HOTEL.EMPRESA.ID_TIPO = 3;
                if (services!= null)
                {
                    db.EMPRESA.Add(cATALOGO_HOTEL.EMPRESA);
                    cATALOGO_HOTEL.ID_EMPRESA = cATALOGO_HOTEL.EMPRESA.ID;
                    foreach(var c in services)
                    {
                        cATALOGO_HOTEL.ID_SERVICIO = c;                            
                        db.CATALOGO_HOTEL.Add(cATALOGO_HOTEL);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Se necesita al menos un servcio");
                    ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE");
                    ViewBag.EMPRESA_ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE");
                    ViewBag.Servicios = db.SERVICIO.ToList();
                    ViewBag.ID_SERVICIO = new SelectList(db.SERVICIO, "ID", "DESCRIPCION", cATALOGO_HOTEL.ID_SERVICIO);
                    return View();
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Ocurrio un error agregando el hotel");
            }
            ViewBag.ID_SERVICIO = new SelectList(db.SERVICIO, "ID", "DESCRIPCION", cATALOGO_HOTEL.ID_SERVICIO);
            ViewBag.EMPRESA_ID_REGION = new SelectList(db.REGION, "ID", "NOMBRE");
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", cATALOGO_HOTEL.ID_EMPRESA);
            ViewBag.Servicios = db.SERVICIO.ToList();
            return View(cATALOGO_HOTEL);
        }

        // GET: CATALOGO_HOTEL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATALOGO_HOTEL cATALOGO_HOTEL = db.CATALOGO_HOTEL.Find(id);
            if (cATALOGO_HOTEL == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", cATALOGO_HOTEL.ID_EMPRESA);
            ViewBag.ID_SERVICIO = new SelectList(db.SERVICIO, "ID", "DESCRIPCION", cATALOGO_HOTEL.ID_SERVICIO);
            return View(cATALOGO_HOTEL);
        }

        // POST: CATALOGO_HOTEL/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VALIDACION,ID_SERVICIO,ID_EMPRESA")] CATALOGO_HOTEL cATALOGO_HOTEL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATALOGO_HOTEL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPRESA = new SelectList(db.EMPRESA, "ID", "NOMBRE", cATALOGO_HOTEL.ID_EMPRESA);
            ViewBag.ID_SERVICIO = new SelectList(db.SERVICIO, "ID", "DESCRIPCION", cATALOGO_HOTEL.ID_SERVICIO);
            return View(cATALOGO_HOTEL);
        }

        // GET: CATALOGO_HOTEL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATALOGO_HOTEL cATALOGO_HOTEL = db.CATALOGO_HOTEL.Find(id);
            if (cATALOGO_HOTEL == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_HOTEL);
        }

        // POST: CATALOGO_HOTEL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATALOGO_HOTEL cATALOGO_HOTEL = db.CATALOGO_HOTEL.Find(id);
            db.CATALOGO_HOTEL.Remove(cATALOGO_HOTEL);
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
