using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using odonto.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace odonto.Controllers
{
    public class PACIENTEController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();


        // GET: PACIENTE
        public ActionResult Index()
        {
            var PACIENTE = db.PACIENTEs.Include(p => p.PACIENTE2);
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(PACIENTE.ToList());
        }

        // GET: PACIENTE/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTEs.Find(id);
            PacienteViewModel model = new PacienteViewModel(pACIENTE);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE", pACIENTE.ID_PACIENTE);
            ViewBag.ID_TRATAMIENTO = new SelectList(db.TRATAMIENTOes, "ID", "NOMBRE");
            return View(model);
        }

        // GET: PACIENTE/Create
        public ActionResult Create()
        {
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE");
            ViewBag.ID_TRATAMIENTO = new SelectList(db.TRATAMIENTOes, "ID", "NOMBRE");
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            PacienteViewModel paci = new PacienteViewModel();
            return View(paci);
        }

        // POST: PACIENTE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,NOMBRE,SEGUNDO_NOMBRE,APELLIDO,SEGUNDO_APELLIDO,FECHA_NACIMIENTO,FECHA_INGRESO,SEXO,ID_PACIENTE,FECHA_MODIFICACION")] PACIENTE pACIENTE)
        public ActionResult Create(PacienteViewModel model)
        {
            if (model != null)
            {
                PACIENTE pa = new PACIENTE(model);
                db.PACIENTEs.Add(pa);
                db.SaveChanges();
                model.ID = pa.ID;
                if (model.seguro != null)
                {
                    foreach (long lo in model.seguro)
                    {
                        PACIENTE_ASEGURADORA pase = new PACIENTE_ASEGURADORA() { ID_PACIENTE = pa.ID, ID_ASEGURADORA = lo, ESTATUS = true, COBERTURA = model.cobertura};
                        db.PACIENTE_ASEGURADORA.Add(pase);
                        db.SaveChanges();
                    }
                }
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
                    RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_PACIENTE = pa.ID, ID_TELEFONO = id };
                    db.RELACION_TELEFONO.Add(rela);
                    db.SaveChanges();
                }
                if (!string.IsNullOrEmpty(model.TELEOFICINA))
                {
                    long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELEOFICINA select tele.ID).SingleOrDefault();
                    TELEFONO te;
                    if (id == 0)
                    {
                        te = new TELEFONO(model, 2);
                        db.TELEFONOes.Add(te);
                        db.SaveChanges();
                        id = te.ID;
                    }
                    RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_PACIENTE = pa.ID, ID_TELEFONO = id };
                    db.RELACION_TELEFONO.Add(rela);
                    db.SaveChanges();
                }
                if (!string.IsNullOrEmpty(model.TELECELULAR))
                {
                    long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELECELULAR select tele.ID).SingleOrDefault();
                    TELEFONO te;
                    if (id == 0)
                    {
                        te = new TELEFONO(model, 3);
                        db.TELEFONOes.Add(te);
                        db.SaveChanges();
                        id = te.ID;
                    }
                    RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_PACIENTE = pa.ID, ID_TELEFONO = id };
                    db.RELACION_TELEFONO.Add(rela);
                    db.SaveChanges();
                }


            }
            else
            {
                model.messageError = "Ocurrio un error inesperado";
            }

            return Json(model);
        }

        // GET: PACIENTE/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //PACIENTE pACIENTE = db.PACIENTEs.Find(id);
            PacienteViewModel pACIENTE = new PacienteViewModel(db.PACIENTEs.Find(id));
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE", pACIENTE.ID_PACIENTE);
            ViewBag.ID_TRATAMIENTO = new SelectList(db.TRATAMIENTOes, "ID", "NOMBRE");
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            return View(pACIENTE);
        }

        // POST: PACIENTE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PacienteViewModel model)
        {
            if (model != null)
            {
                PACIENTE pa = new PACIENTE(model);
                pa.FECHA_MODIFICACION = DateTime.Now;
                db.Entry(pa).State = EntityState.Modified;
                db.SaveChanges();

                List<paciente_telefono> lista = (from tele in db.RELACION_TELEFONO
                                                 where tele.ID_PACIENTE == model.ID
                                                 select new paciente_telefono
                                                 {
                                                     ID = tele.ID,
                                                     ID_TELEFONO = tele.ID_TELEFONO,
                                                     ID_PACIENTE = tele.ID_PACIENTE,
                                                     ID_DOCTOR = tele.ID_DOCTOR,
                                                     ID_GERENTE = tele.ID_GERENTE,
                                                     ID_SECRETARIA = tele.ID_SECRETARIA
                                                 }).ToList();
                
                    if (!string.IsNullOrEmpty(model.TELECASA))
                    {
                        TELEFONO te = null;
                        if (lista != null)
                        {
                            foreach (paciente_telefono ppa in lista)
                            {
                                if (ppa.telefono.TIPOCHECK == "Telefono de Casa")
                                {
                                    te = new TELEFONO(ppa.telefono);
                                }
                            }
                        }
                            
                            if (te != null)
                            {
                                te.NUMERO = model.TELECASA;
                                db.Entry(te).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELECASA select tele.ID).SingleOrDefault();
                                if (id == 0)
                                {
                                    te = new TELEFONO(model, 1);
                                    db.TELEFONOes.Add(te);
                                    db.SaveChanges();
                                    id = te.ID;
                                }
                                RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_PACIENTE = pa.ID, ID_TELEFONO = id };
                                db.RELACION_TELEFONO.Add(rela);
                                db.SaveChanges();
                        }                                               
                    }
                    if (!string.IsNullOrEmpty(model.TELEOFICINA))
                    {
                        TELEFONO te = null;
                        if (lista != null)
                        {
                            foreach (paciente_telefono ppa in lista)
                            {
                                if (ppa.telefono.TIPOCHECK == "Telefono de Oficina")
                                {
                                    te = new TELEFONO(ppa.telefono);
                                }
                            }
                        }
                    if (te != null)
                    {
                        te.NUMERO = model.TELEOFICINA;
                        db.Entry(te).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELEOFICINA select tele.ID).SingleOrDefault();
                        if (id == 0)
                        {
                            te = new TELEFONO(model, 2);
                            db.TELEFONOes.Add(te);
                            db.SaveChanges();
                            id = te.ID;
                        }
                        RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_PACIENTE = pa.ID, ID_TELEFONO = id };
                        db.RELACION_TELEFONO.Add(rela);
                        db.SaveChanges();
                    }
                    }
                if (!string.IsNullOrEmpty(model.TELECELULAR))
                {
                    TELEFONO te = null;
                    if (lista != null)
                    {
                        foreach (paciente_telefono ppa in lista)
                        {
                            if (ppa.telefono.TIPOCHECK == "Telefono Celular")
                            {
                                te = new TELEFONO(ppa.telefono);
                            }
                        }
                    }
                    if (te != null)
                    {
                        te.NUMERO = model.TELECELULAR;
                        db.Entry(te).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        long id = (from tele in db.TELEFONOes where tele.NUMERO == model.TELECELULAR select tele.ID).SingleOrDefault();
                        if (id == 0)
                        {
                            te = new TELEFONO(model, 3);
                            db.TELEFONOes.Add(te);
                            db.SaveChanges();
                            id = te.ID;
                        }
                        RELACION_TELEFONO rela = new RELACION_TELEFONO() { ID_PACIENTE = pa.ID, ID_TELEFONO = id };
                        db.RELACION_TELEFONO.Add(rela);
                        db.SaveChanges();
                    }
                }
                
            }
            else
            {
                model.messageError = "Ocurrio un error inesperado";
            }
            
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTEs, "ID", "NOMBRE", model.ID_PACIENTE);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnfermedadPaciente(PacienteViewModel model)
        {
            if (model != null)
            {
                foreach (PACIENTE_ENFERMEDAD pa in model.Enfermedades)
                {
                    db.PACIENTE_ENFERMEDAD.Add(pa);
                }
                db.SaveChanges();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult 
            Enfermedad(PacienteViewModel model)
        {
            if (model != null)
            {
                PACIENTE pa = db.PACIENTEs.Find(model.ID);

                if (pa.PACIENTE_ENFERMEDAD != null && pa.PACIENTE_ENFERMEDAD.Count != 0)
                {
                    List<PACIENTE_ENFERMEDAD> listDelete = new List<PACIENTE_ENFERMEDAD>();
                    foreach (PACIENTE_ENFERMEDAD pe in pa.PACIENTE_ENFERMEDAD)
                    {
                        if (model.Enfermedades != null && model.Enfermedades.Count != 0)
                        {
                            foreach (PACIENTE_ENFERMEDAD enf in model.Enfermedades)
                            {
                                if (pe.ID == enf.ID)
                                {
                                    listDelete.Add(pe);
                                }
                            }
                        }
                    }

                    if (listDelete != null && listDelete.Count != 0)
                    {
                        foreach(PACIENTE_ENFERMEDAD pe in listDelete)
                        {
                            db.PACIENTE_ENFERMEDAD.Remove(pe);
                            db.SaveChanges();
                        }
                    }
                    foreach (PACIENTE_ENFERMEDAD pen in model.Enfermedades)
                    {
                        if (pen.ID == 0)
                        {
                            db.PACIENTE_ENFERMEDAD.Add(pen);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    if (model.Enfermedades != null && model.Enfermedades.Count != 0)
                    {
                        foreach (PACIENTE_ENFERMEDAD pe in model.Enfermedades)
                        {
                            db.PACIENTE_ENFERMEDAD.Add(pe);
                        }
                        db.SaveChanges();
                    }
                }

            }
            else
            {
                model.messageError = "Ocurrio un error inesperado.";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: PACIENTE/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTEs.Find(id);
            PacienteViewModel model = new PacienteViewModel(pACIENTE);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View(model);
        }

        // POST: PACIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PACIENTE pACIENTE = db.PACIENTEs.Find(id);
            db.PACIENTEs.Remove(pACIENTE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ObtenerEnfermedad(string id)
        {
            List<ENFERMEDAD> lista;
            SqlParameter param1 = new SqlParameter("@id", id);
            using (db)
            {
                lista = db.Database.SqlQuery<ENFERMEDAD>("ObtenerEnfermedad @id", param1).ToList();
            }
                

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EnfermedadModal([Bind(Include = "NOMBRE,DESCRIPCION,TIPO")] ENFERMEDAD modal)
        {
            if (ModelState.IsValid)
            {
                db.ENFERMEDADs.Add(modal);
                db.SaveChanges();
            }
            else
            {
                string mensaje = "Ocurrio un error inesperado";
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }

            return Json(modal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerDiente(string nombre) {
            PacienteViewModel model = new PacienteViewModel();
            DIENTE diente = new DIENTE();

            if (!string.IsNullOrEmpty(nombre))
            {
               diente = db.DIENTEs.Where(d => d.NOMBRE == nombre).FirstOrDefault();
               model.svg = diente.SVG;
            }
            else
            {
                model.messageError = "Ocurrion un error inesperado";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerColor(long id)
        {
            TRATAMIENTO tra = db.TRATAMIENTOes.Find(id);
            string valor = tra.COLOR;
            return Json(valor, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult CrearHistorial(paciente_historial historial)
        {
            if (historial != null)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (historial.id == 0)
                        {
                            HISTORIAL hi = new HISTORIAL(historial);
                            db.HISTORIALs.Add(hi);
                            db.SaveChanges();
                            historial.id = hi.ID;

                            if (historial.listaDetalle != null && historial.listaDetalle.Count != 0)
                            {
                                foreach (Detalle de in historial.listaDetalle)
                                {
                                    TRATA_DIENTE tr = db.TRATA_DIENTE.SqlQuery("select td.* from TRATAMIENTO tr, TRATA_DIENTE td, DIENTE d where td.ID_TRATAMIENTO = " + de.id_tratamiento + " and td.ID_DIENTE = (select ID from DIENTE where NOMBRE='" + hi.DIENTE + "')").FirstOrDefault<TRATA_DIENTE>();
                                    de.id_trata_diente = tr.ID;
                                    de.id_historial = hi.ID;
                                    DETALLE_HISTORIAL detalle = new DETALLE_HISTORIAL(de);
                                    db.DETALLE_HISTORIAL.Add(detalle);
                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            List<DETALLE_HISTORIAL> de = db.DETALLE_HISTORIAL.SqlQuery("SELECT * FROM DETALLE_HISTORIAL WHERE ID_HISTORIAL =" + historial.id).ToList();
                            if (de != null && de.Count != 0)
                            {
                                foreach (DETALLE_HISTORIAL dh in de)
                                {
                                    bool validate = false;
                                    if (historial.listaDetalle != null && historial.listaDetalle.Count != 0)
                                    {
                                        foreach (Detalle dd in historial.listaDetalle)
                                        {
                                            if (dh.ID == dd.id)
                                            {
                                                validate = true;
                                            }
                                        }
                                    }

                                    if (validate == false)
                                    {
                                        db.DETALLE_HISTORIAL.Remove(dh);
                                        db.SaveChanges();
                                    }
                                }
                            }

                            if (historial.listaDetalle != null && historial.listaDetalle.Count != 0)
                            {
                                foreach (Detalle total in historial.listaDetalle)
                                {
                                    TRATA_DIENTE tr = db.TRATA_DIENTE.SqlQuery("select td.* from TRATAMIENTO tr, TRATA_DIENTE td, DIENTE d where td.ID_TRATAMIENTO = " + total.id_tratamiento + " and td.ID_DIENTE = (select ID from DIENTE where NOMBRE='" + historial.diente + "')").FirstOrDefault<TRATA_DIENTE>();
                                    total.id_trata_diente = tr.ID;
                                    total.id_historial = historial.id;
                                    DETALLE_HISTORIAL detalle = new DETALLE_HISTORIAL(total);
                                    db.DETALLE_HISTORIAL.Add(detalle);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                HISTORIAL hi = db.HISTORIALs.Find(historial.id);
                                db.HISTORIALs.Remove(hi);
                                db.SaveChanges();
                            }
                        }

                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
                    
            }

            return Json(historial, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistorial(long id, string diente)
        {
            
            HISTORIAL historial = null;
            paciente_historial hist = null;

            if (id != 0 && !string.IsNullOrEmpty(diente))
            {
                historial = db.HISTORIALs.SqlQuery("SELECT * FROM HISTORIAL WHERE ID_PACIENTE =" + id + " AND DIENTE = '" + diente + "'").FirstOrDefault();
                if (historial != null)
                {
                    hist = new paciente_historial(historial);
                }
            }

            return Json(hist, JsonRequestBehavior.AllowGet);
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
