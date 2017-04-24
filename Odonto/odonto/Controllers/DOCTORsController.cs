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
    public class DOCTORController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        // GET: DOCTOR
        public ActionResult Index()
        {
            var DOCTOR = db.DOCTORs.Include(d => d.ROL);
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(DOCTOR.ToList());
        }

        // GET: DOCTOR/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTOR dOCTOR = db.DOCTORs.Find(id);
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(dOCTOR);
        }

        // GET: DOCTOR/Create
        public ActionResult Create()
        {
            //ViewBag.ID_ROL = new SelectList(db.ROLs, "ID", "NOMBRE");
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            DoctorViewModel model = new DoctorViewModel();
            return View(model);
        }

        // POST: DOCTOR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorViewModel model)
        {
            if (model != null)
            {
                DOCTOR doctor = new DOCTOR(model);
                db.DOCTORs.Add(doctor);
                db.SaveChanges();
                model.ID = doctor.ID;

                if (!string.IsNullOrEmpty(model.TELECASA))
                {
                    long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELECASA select tele.ID).SingleOrDefault();
                    TELEFONO te;
                    if (id == 0)
                    {
                        te = new TELEFONO(model, 1);
                        db.TELEFONOes.Add(te);
                        db.SaveChanges();
                        id = te.ID;
                    }
                    RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_DOCTOR = doctor.ID, ID_TELEFONO = id };
                    db.RELACION_TELEFONO.Add(rela);
                    db.SaveChanges();
                }
                if (!string.IsNullOrEmpty(model.TELEOFICINA))
                {
                    long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELECASA select tele.ID).SingleOrDefault();
                    TELEFONO te;
                    if (id == 0)
                    {
                        te = new TELEFONO(model, 2);
                        db.TELEFONOes.Add(te);
                        db.SaveChanges();
                        id = te.ID;
                    }
                    RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_DOCTOR = doctor.ID, ID_TELEFONO = id };
                    db.RELACION_TELEFONO.Add(rela);
                    db.SaveChanges();
                }
                if (!string.IsNullOrEmpty(model.TELECELULAR))
                {
                    long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELECASA select tele.ID).SingleOrDefault();
                    TELEFONO te;
                    if (id == 0)
                    {
                        te = new TELEFONO(model, 3);
                        db.TELEFONOes.Add(te);
                        db.SaveChanges();
                        id = te.ID;
                    }
                    RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_DOCTOR = doctor.ID, ID_TELEFONO = id };
                    db.RELACION_TELEFONO.Add(rela);
                    db.SaveChanges();
                }
            }
            else
            {
                model.messageError = "Ocurrio un error inesperado.";
            }

            return Json(model,JsonRequestBehavior.AllowGet);
        }

        // GET: DOCTOR/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DOCTOR dOCTOR = db.DOCTORs.Find(id);
            DoctorViewModel dOCTOR = new DoctorViewModel(db.DOCTORs.Find(id));
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ROL = new SelectList(db.ROLs, "ID", "NOMBRE", dOCTOR.ID_ROL);
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(dOCTOR);
        }

        // POST: DOCTOR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_ROL,NOMBRE,SEGUNDO_NOMBRE,APELLIDO,SEGUNDO_APELLIDO,LOGIN,PASSWORD,EMAIL,ESTATUS,FECHA_MODIFICACION")] DOCTOR dOCTOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOCTOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ROL = new SelectList(db.ROLs, "ID", "NOMBRE", dOCTOR.ID_ROL);
            return View(dOCTOR);
        }

        // GET: DOCTOR/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTOR dOCTOR = db.DOCTORs.Find(id);
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(dOCTOR);
        }

        // POST: DOCTOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DOCTOR dOCTOR = db.DOCTORs.Find(id);
            db.DOCTORs.Remove(dOCTOR);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult CreateContrato(DoctorViewModel doctor)
        {
            if (doctor.listaContratos.Count != 0 && doctor.listaContratos != null)
            {
                if (doctor.ID != 0)
                {
                    foreach (DoctorViewModel doc in doctor.listaContratos)
                    {
                        CONTRATO contrato = new CONTRATO(doc);
                        contrato.ESTATUS = true;
                        db.CONTRATOes.Add(contrato);
                        db.SaveChanges();
                    }
                }
                else
                {
                    doctor.messageError = "Debe crear al doctor antes de insertar el contrato.";
                }
            }
            else
            {
                doctor.messageError = "Ocurrio un error inesperado.";
            }
            return Json(doctor, JsonRequestBehavior.AllowGet);
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
