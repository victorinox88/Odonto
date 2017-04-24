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
    public class TRATAMIENTOController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        // GET: TRATAMIENTO
        public ActionResult Index()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(db.TRATAMIENTOes.ToList());
        }

        // GET: TRATAMIENTO/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRATAMIENTO tRATAMIENTO = db.TRATAMIENTOes.Find(id);
            if (tRATAMIENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(tRATAMIENTO);
        }

        // GET: TRATAMIENTO/Create
        public ActionResult Create()
        {
            TratamientoViewModel tra = new TratamientoViewModel();
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(tra);
        }

        // POST: TRATAMIENTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(TratamientoViewModel model)
        {
            BaseViewModel my = new BaseViewModel();
            if (model != null && model.listaTratamiento.Count != 0 && model.listaTratamiento != null)
            {
                foreach (TratamientoViewModel tr in model.listaTratamiento)
                {
                    long id = (from tele in db.TRATAMIENTOes where tele.NOMBRE == model.NOMBRE select tele.ID).SingleOrDefault();

                    if (id == 0)
                    {
                        TRATAMIENTO tratamiento = new TRATAMIENTO(tr);
                        tratamiento.ESTATUS = true;
                        db.TRATAMIENTOes.Add(tratamiento);
                        db.SaveChanges();
                        id = tratamiento.ID;
                    }

                    if (tr.listaTraDien != null)
                    {
                        foreach (TRATA_DIENTE td in tr.listaTraDien)
                        {
                            td.ID_TRATAMIENTO = id;
                            db.TRATA_DIENTE.Add(td);
                            db.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                my.messageError = "Ocurrio un error inesperado";
            }

            return Json(my, JsonRequestBehavior.AllowGet);
        }

        // GET: TRATAMIENTO/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRATAMIENTO tRATAMIENTO = db.TRATAMIENTOes.Find(id);
            tRATAMIENTO.FECHA_MODIFICACION = DateTime.Now;
            if (tRATAMIENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(tRATAMIENTO);
        }

        // POST: TRATAMIENTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,DESCRIPCION,ESTATUS,COLOR,MONTO")] TRATAMIENTO tRATAMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRATAMIENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRATAMIENTO);
        }

        // GET: TRATAMIENTO/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRATAMIENTO tRATAMIENTO = db.TRATAMIENTOes.Find(id);
            if (tRATAMIENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(tRATAMIENTO);
        }

        // POST: TRATAMIENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TRATAMIENTO tRATAMIENTO = db.TRATAMIENTOes.Find(id);
            db.TRATAMIENTOes.Remove(tRATAMIENTO);
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
