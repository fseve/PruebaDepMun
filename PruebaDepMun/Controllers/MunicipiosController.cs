using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PruebaDepMun.DAL;

namespace PruebaDepMun.Controllers
{
    public class MunicipiosController : Controller
    {
        private PruebaDepMunEntities db = new PruebaDepMunEntities();

        // GET: Municipios
        public ActionResult Index()
        {
            var municipios = db.Municipios.Include(m => m.Departamentos);
            return View(municipios.ToList());
        }

        // GET: Municipios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
            ViewBag.iddep = new SelectList(db.Departamentos, "id", "nombre");
            return View();
        }

        // POST: Municipios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,iddep")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                db.Municipios.Add(municipios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iddep = new SelectList(db.Departamentos, "id", "nombre", municipios.iddep);
            return View(municipios);
        }

        // GET: Municipios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            ViewBag.iddep = new SelectList(db.Departamentos, "id", "nombre", municipios.iddep);
            return View(municipios);
        }

        // POST: Municipios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,iddep")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iddep = new SelectList(db.Departamentos, "id", "nombre", municipios.iddep);
            return View(municipios);
        }

        // GET: Municipios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Municipios municipios = db.Municipios.Find(id);
            db.Municipios.Remove(municipios);
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
