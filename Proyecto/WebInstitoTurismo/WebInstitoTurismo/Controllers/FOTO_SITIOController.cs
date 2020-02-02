using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebInstitoTurismo.Models;

namespace WebInstitoTurismo.Controllers
{
    public class FOTO_SITIOController : Controller
    {
        private INSTITUTO_DE_TURISMOEntities db = new INSTITUTO_DE_TURISMOEntities();
        // GET: FOTO_SITIO
        public ActionResult Index(String id)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM SITIO_TURISTICO WHERE ID NOT IN(SELECT ID_SITIO FROM FOTO_SITIO);");
            var fOTO_SITIO = db.FOTO_SITIO.Include(f => f.SITIO_TURISTICO);
            if (!String.IsNullOrEmpty(id))
            {
                fOTO_SITIO = fOTO_SITIO.Where(f => f.SITIO_TURISTICO.ID.ToString().Equals(id));
                ViewData["IdSitio"] = id;
            }

            var ID_SITIO = from s in fOTO_SITIO
                           group s by s.SITIO_TURISTICO into newGroup
                           where newGroup.Key != null
                           select new { nombre = newGroup.Key };

            ViewBag.ID_SITIO = ID_SITIO.Select(m=> new SelectListItem {Value=m.nombre.ID.ToString(), Text=m.nombre.NOMBRE});

            return View(fOTO_SITIO.ToList());
        }

        // GET: FOTO_SITIO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOTO_SITIO fOTO_SITIO = db.FOTO_SITIO.Find(id);
            if (fOTO_SITIO == null)
            {
                return HttpNotFound();
            }
            return View(fOTO_SITIO);
        }

        // GET: FOTO_SITIO/Create
        public ActionResult Create(int? id)
        {
            if (!(id==null))
            {
                ViewBag.ID_SITIO = new SelectList(db.SITIO_TURISTICO.Where(p=> p.ID.ToString().Equals(id.ToString())), "ID", "NOMBRE");
            }
            else{
                ViewBag.ID_SITIO = new SelectList(db.SITIO_TURISTICO, "ID", "NOMBRE");
            }
            return View();
        }

        // POST: FOTO_SITIO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DIRECCION,TITULO,ID_SITIO")] FOTO_SITIO fOTO_SITIO)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            try
            {
                WebImage image = new WebImage(FileBase.InputStream);
                fOTO_SITIO.DIRECCION = image.GetBytes();
            }
            catch
            {
                ModelState.AddModelError("", "Es Obligatorio llenar el campo foto");
            }

            if (ModelState.IsValid)
            {
                db.FOTO_SITIO.Add(fOTO_SITIO);
                db.SaveChanges();
                ModelState.AddModelError("", "Se agrego la foto, puede subir mas si lo desea");
                return RedirectToAction("Create/" + fOTO_SITIO.ID_SITIO.ToString());                
            }
                        
            ViewBag.ID_SITIO = new SelectList(db.SITIO_TURISTICO, "ID", "NOMBRE", fOTO_SITIO.ID_SITIO);
            return View(fOTO_SITIO);
        }

        // GET: FOTO_SITIO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOTO_SITIO fOTO_SITIO = db.FOTO_SITIO.Find(id);
            if (fOTO_SITIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_SITIO = new SelectList(db.SITIO_TURISTICO, "ID", "NOMBRE", fOTO_SITIO.ID_SITIO);
            return View(fOTO_SITIO);
        }

        // POST: FOTO_SITIO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DIRECCION,TITULO,ID_SITIO")] FOTO_SITIO fOTO_SITIO)
        {
            byte[] imagenActual = null;
            HttpPostedFileBase FileBase = Request.Files[0];
           
            if(FileBase == null)
            {
                imagenActual = db.FOTO_SITIO.SingleOrDefault(t => t.ID == fOTO_SITIO.ID).DIRECCION;
            }
            else
            {
                WebImage image = new WebImage(FileBase.InputStream);
                fOTO_SITIO.DIRECCION = image.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Entry(fOTO_SITIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_SITIO = new SelectList(db.SITIO_TURISTICO, "ID", "NOMBRE", fOTO_SITIO.ID_SITIO);
            return View(fOTO_SITIO);
        }

        // GET: FOTO_SITIO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOTO_SITIO fOTO_SITIO = db.FOTO_SITIO.Find(id);
            if (fOTO_SITIO == null)
            {
                return HttpNotFound();
            }
            return View(fOTO_SITIO);
        }

        // POST: FOTO_SITIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FOTO_SITIO fOTO_SITIO = db.FOTO_SITIO.Find(id);
            db.FOTO_SITIO.Remove(fOTO_SITIO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(int id)
        {
            FOTO_SITIO fotoSitio = db.FOTO_SITIO.Find(id);
            byte[] byteImage = fotoSitio.DIRECCION;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
           
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
