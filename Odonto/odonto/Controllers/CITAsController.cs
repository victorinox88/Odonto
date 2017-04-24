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
    public class CITAController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        // GET: CITA
        public ActionResult Index()
        {
            var CITA = db.CITAs.Include(c => c.DOCTOR).Include(c => c.PACIENTE);
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(CITA.ToList());
        }

        // GET: CITA/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITA cITA = db.CITAs.Find(id);
            if (cITA == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(cITA);
        }

        // GET: CITA/Create
        public ActionResult Create()
        {
            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE");
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE");
            HttpCookie cookie = new HttpCookie("usuario");
            cookie["id"] = cookieName["id"];
            cookie["nombre"] = cookieName["nombre"];
            cookie["apellido"] = cookieName["apellido"];
            cookie["rol"] = cookieName["rol"];
            cookie["calendario"] = "Luisa2";
            Response.Cookies.Remove("usuario");
            Response.Cookies.Add(cookie);
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View();
        }

        public ActionResult Detalle(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITA cita = db.CITAs.Find(id);
            PacienteViewModel model = new PacienteViewModel(db.PACIENTEs.Find(cita.ID_PACIENTE));
            model.cita = cita;
            ViewBag.ID_TRATAMIENTO = new SelectList(db.TRATAMIENTOes, "ID", "NOMBRE");

            return View(model);
        }

        // POST: CITA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CitaViewModel model)
        {
            CITA cita = null;

            if (model != null)
            {
                cita = new CITA(model);
                cita.ESTATUS = true;
                db.CITAs.Add(cita);
                db.SaveChanges();
            }
            else
            {
                model.messageError = "Ocurrio un error inesperado";
            }

            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE", cita.ID_DOCTOR);
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE", cita.ID_PACIENTE);

            return Json(model,JsonRequestBehavior.AllowGet);
        }

        // GET: CITA/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITA cITA = db.CITAs.Find(id);
            CitaViewModel cita = new CitaViewModel(cITA);
            if (cITA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE", cITA.ID_DOCTOR);
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE", cITA.ID_PACIENTE);
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(cita);
        }

        // POST: CITA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CitaViewModel model)
        {
            CITA cita = null;

            if (model != null)
            {
                cita = new CITA(model);
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                model.messageError = "Ocurrio un error inesperado";
            }
          
            ViewBag.ID_DOCTOR = new SelectList(db.DOCTORs, "ID", "NOMBRE", cita.ID_DOCTOR);
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE", cita.ID_PACIENTE);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: CITA/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITA cITA = db.CITAs.Find(id);
            if (cITA == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(cITA);
        }

        // POST: CITA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CITA cITA = db.CITAs.Find(id);
            db.CITAs.Remove(cITA);
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
