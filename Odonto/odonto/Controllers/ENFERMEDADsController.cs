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
    public class ENFERMEDADController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        // GET: ENFERMEDAD
        public ActionResult Index()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(db.ENFERMEDADs.ToList());
        }

        // GET: ENFERMEDAD/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENFERMEDAD eNFERMEDAD = db.ENFERMEDADs.Find(id);
            if (eNFERMEDAD == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(eNFERMEDAD);
        }

        // GET: ENFERMEDAD/Create
        public ActionResult Create()
        {
            EnfermedadViewModel enfer = new EnfermedadViewModel();
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(enfer);
        }

        // POST: ENFERMEDAD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnfermedadViewModel model)
        {
            if (model.listaEnfermedad != null && model.listaEnfermedad.Count() != 0)
            {
                ENFERMEDAD val = null;
                foreach(ENFERMEDAD ef in model.listaEnfermedad)
                {
                    val = db.ENFERMEDADs.Where(b => b.NOMBRE == ef.NOMBRE).FirstOrDefault();
                    if (val == null)
                    { 
                        db.ENFERMEDADs.Add(ef);
                    }
                    else
                    {
                        model.messageError = "Una de las enfermedades que intenta registrar ya se encuentra registrada en el sistema.";
                    }
                }
                if (val == null)
                {
                    db.SaveChanges();
                }
                //return RedirectToAction("Index");
            }
            else
            {
                model.messageError = "Ocurrio un error inesperdado.";
            }

            return Json(model,JsonRequestBehavior.AllowGet);
        }

        // GET: ENFERMEDAD/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EnfermedadViewModel model = null;
            ENFERMEDAD eNFERMEDAD = db.ENFERMEDADs.Find(id);
            eNFERMEDAD.FECHA_MODIFICACION = DateTime.Now;
            
            if (eNFERMEDAD != null)
            {
                model = new EnfermedadViewModel(eNFERMEDAD);
            }
            else
            {
                model = new EnfermedadViewModel();
                model.messageError = "Ocurrió un error inesperado.";
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(model);
        }

        // POST: ENFERMEDAD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EnfermedadViewModel model)
        {
            if (model != null)
            {
                if (model.TIPO != "0" && !string.IsNullOrEmpty(model.NOMBRE))
                {
                    ENFERMEDAD eNFERMEDAD = new ENFERMEDAD(model);
                    db.Entry(eNFERMEDAD).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    model.messageError = "Se deben rellenar todos los campos.";
                }
            }
            else
            {
                model.messageError = "Ocurrio un error inesperado modificando la enfermdad.";
            }
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        // GET: ENFERMEDAD/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENFERMEDAD eNFERMEDAD = db.ENFERMEDADs.Find(id);
            if (eNFERMEDAD == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(eNFERMEDAD);
        }

        // POST: ENFERMEDAD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ENFERMEDAD eNFERMEDAD = db.ENFERMEDADs.Find(id);
            db.ENFERMEDADs.Remove(eNFERMEDAD);
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
