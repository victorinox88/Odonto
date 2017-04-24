using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using odonto.Models;

namespace odonto.Controllers
{
    public class CONTRATOController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        // GET: CONTRATO
        public ActionResult Index()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            var cONTRATOes = db.CONTRATOes.Include(c => c.ASEGURADORA).Include(c => c.DOCTOR).Include(c => c.INSTITUCION);
            return View(cONTRATOes.ToList());
        }

        // GET: CONTRATO/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATO cONTRATO = db.CONTRATOes.Find(id);
            if (cONTRATO == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(cONTRATO);
        }

        // GET: CONTRATO/Create
        public ActionResult Create()
        {
            ContratoViewModel model = new ContratoViewModel();
            ViewBag.ID_ASEGURADORA = new SelectList(db.ASEGURADORAs, "ID", "NOMBRE");
            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE");
            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONs, "ID", "NOMBRE");
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(model);
        }

        // POST: CONTRATO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_DOCTOR,ID_INSTITUCION,ID_ASEGURADORA,FECHA_INICIO,FECHA_FIN,ESTATUS,FECHA_MODIFICACION")] CONTRATO cONTRATO)
        {
            if (ModelState.IsValid)
            {
                db.CONTRATOes.Add(cONTRATO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ASEGURADORA = new SelectList(db.ASEGURADORAs, "ID", "NOMBRE", cONTRATO.ID_ASEGURADORA);
            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE", cONTRATO.ID_DOCTOR);
            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONs, "ID", "NOMBRE", cONTRATO.ID_INSTITUCION);
            return View(cONTRATO);
        }

        // GET: CONTRATO/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATO cONTRATO = db.CONTRATOes.Find(id);
            ContratoViewModel model = new ContratoViewModel(cONTRATO);
            if (cONTRATO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ASEGURADORA = new SelectList(db.ASEGURADORAs, "ID", "NOMBRE", cONTRATO.ID_ASEGURADORA);
            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE", cONTRATO.ID_DOCTOR);
            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONs, "ID", "NOMBRE", cONTRATO.ID_INSTITUCION);
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(model);
        }

        // POST: CONTRATO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_DOCTOR,ID_INSTITUCION,ID_ASEGURADORA,FECHA_INICIO,FECHA_FIN,ESTATUS,FECHA_MODIFICACION")] CONTRATO cONTRATO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTRATO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ASEGURADORA = new SelectList(db.ASEGURADORAs, "ID", "NOMBRE", cONTRATO.ID_ASEGURADORA);
            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE", cONTRATO.ID_DOCTOR);
            ViewBag.ID_INSTITUCION = new SelectList(db.INSTITUCIONs, "ID", "NOMBRE", cONTRATO.ID_INSTITUCION);
            return View(cONTRATO);
        }

        // GET: CONTRATO/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATO cONTRATO = db.CONTRATOes.Find(id);
            if (cONTRATO == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(cONTRATO);
        }

        // POST: CONTRATO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CONTRATO cONTRATO = db.CONTRATOes.Find(id);
            db.CONTRATOes.Remove(cONTRATO);
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
