using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Almacen.Models;

namespace Almacen.Controllers
{
    public class EquiposController : Controller
    {
        private CURSONETEntities db = new CURSONETEntities();

        // GET: Equipos
        public ActionResult Index()
        {
            var equipos = db.Equipos.Include(e => e.Almacenes).Include(e => e.Marcas).Include(e => e.Modelos).Include(e => e.Proveedores);
            return View(equipos.ToList());
        }

        // GET: Equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.iIdAlmacen = new SelectList(db.Almacenes, "iIdAlmacen", "vchClave");
            ViewBag.iIdMarca = new SelectList(db.Marcas, "iIdMarca", "vchNombre");
            ViewBag.iIdModelo = new SelectList(db.Modelos, "iIdModelo", "vchNombre");
            ViewBag.iIdProveedor = new SelectList(db.Proveedores, "iIdProveedor", "vchNombre");
            return View();
        }

        // POST: Equipos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iIdEquipo,iIdMarca,iIdModelo,vchNoSerie,iExistencia,iIdProveedor,iIdAlmacen")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iIdAlmacen = new SelectList(db.Almacenes, "iIdAlmacen", "vchClave", equipos.iIdAlmacen);
            ViewBag.iIdMarca = new SelectList(db.Marcas, "iIdMarca", "vchNombre", equipos.iIdMarca);
            ViewBag.iIdModelo = new SelectList(db.Modelos, "iIdModelo", "vchNombre", equipos.iIdModelo);
            ViewBag.iIdProveedor = new SelectList(db.Proveedores, "iIdProveedor", "vchNombre", equipos.iIdProveedor);
            return View(equipos);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.iIdAlmacen = new SelectList(db.Almacenes, "iIdAlmacen", "vchClave", equipos.iIdAlmacen);
            ViewBag.iIdMarca = new SelectList(db.Marcas, "iIdMarca", "vchNombre", equipos.iIdMarca);
            ViewBag.iIdModelo = new SelectList(db.Modelos, "iIdModelo", "vchNombre", equipos.iIdModelo);
            ViewBag.iIdProveedor = new SelectList(db.Proveedores, "iIdProveedor", "vchNombre", equipos.iIdProveedor);
            return View(equipos);
        }

        // POST: Equipos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iIdEquipo,iIdMarca,iIdModelo,vchNoSerie,iExistencia,iIdProveedor,iIdAlmacen")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iIdAlmacen = new SelectList(db.Almacenes, "iIdAlmacen", "vchClave", equipos.iIdAlmacen);
            ViewBag.iIdMarca = new SelectList(db.Marcas, "iIdMarca", "vchNombre", equipos.iIdMarca);
            ViewBag.iIdModelo = new SelectList(db.Modelos, "iIdModelo", "vchNombre", equipos.iIdModelo);
            ViewBag.iIdProveedor = new SelectList(db.Proveedores, "iIdProveedor", "vchNombre", equipos.iIdProveedor);
            return View(equipos);
        }

        // GET: Equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipos equipos = db.Equipos.Find(id);
            db.Equipos.Remove(equipos);
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
