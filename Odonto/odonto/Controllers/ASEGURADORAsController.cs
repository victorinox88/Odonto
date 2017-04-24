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
    public class ASEGURADORAController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        // GET: ASEGURADORA
        public ActionResult Index()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(db.ASEGURADORAs.ToList());
        }

        // GET: ASEGURADORA/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASEGURADORA aSEGURADORA = db.ASEGURADORAs.Find(id);
            if (aSEGURADORA == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(aSEGURADORA);
        }

        // GET: ASEGURADORA/Create
        public ActionResult Create()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View();
        }

        // POST: ASEGURADORA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(AseguradoraViewModel model)
        {
            if (model != null)
            {
                if (model.listaAseguradora != null && model.listaAseguradora.Count != 0)
                {
                    ASEGURADORA val = null;
                    foreach (ASEGURADORA ase in model.listaAseguradora)
                    {
                        ase.ESTATUS = true;
                        val = db.ASEGURADORAs.Where(b => b.NOMBRE == ase.NOMBRE).FirstOrDefault();
                        if (val != null)
                        {
                            model.messageError = "Uno de las ASEGURADORA que desea registrar ya se encuentra en el sistema.";
                        }
                        else
                        {
                            db.ASEGURADORAs.Add(ase);
                        }
                    }
                    if (val == null)
                    {
                        db.SaveChanges();
                    }
                }
                else
                {
                    model.messageError = "Ocurrio un error inesperado.";
                }
            }

            return Json(model,JsonRequestBehavior.AllowGet);
        }

        // GET: ASEGURADORA/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASEGURADORA aSEGURADORA = db.ASEGURADORAs.Find(id);
            aSEGURADORA.FECHA_MODIFICACION = DateTime.Now;
            if (aSEGURADORA == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(aSEGURADORA);
        }

        // POST: ASEGURADORA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE,DESCRIPCION,ESTATUS,FECHA_MODIFICACION")] ASEGURADORA aSEGURADORA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aSEGURADORA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aSEGURADORA);
        }

        // GET: ASEGURADORA/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASEGURADORA aSEGURADORA = db.ASEGURADORAs.Find(id);
            if (aSEGURADORA == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(aSEGURADORA);
        }

        // POST: ASEGURADORA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ASEGURADORA aSEGURADORA = db.ASEGURADORAs.Find(id);
            db.ASEGURADORAs.Remove(aSEGURADORA);
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
