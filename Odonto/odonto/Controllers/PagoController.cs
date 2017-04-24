using odonto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace odonto.Controllers
{
    public class PagoController : BaseController
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        // GET: Pago
        public ActionResult Index()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            List<RECIBO> recibo = db.RECIBOes.ToList<RECIBO>();
            if (cookieName.Values["id"] == "all")
            {
                if (recibo != null && recibo.Count != 0)
                {
                    List<CITA> ci = db.CITAs.ToList<CITA>();
                    List<CITA> arr = new List<CITA>();
                    foreach (CITA cc in ci)
                    {
                        bool vali = false;
                        foreach (RECIBO re in recibo)
                        {
                            if (re.ID_CITA == cc.ID)
                            {
                                vali = true;
                            }
                        }
                        if (vali == false)
                        {
                            arr.Add(cc);
                        }
                    }

                    ViewBag.ID_CITA = new SelectList(arr.Select(s => new { ID = s.ID, NOMBRE = string.Format("{0} {1} {2}", s.PACIENTE.NOMBRE, s.PACIENTE.APELLIDO, s.FECHA.ToString("dd/MM/yyyy")) }), "ID", "NOMBRE");
                }
                else
                {
                    ViewBag.ID_CITA = new SelectList(db.CITAs.SqlQuery("select * from CITA").ToList().Select(s => new { ID = s.ID, NOMBRE = string.Format("{0} {1} {2}", s.PACIENTE.NOMBRE, s.PACIENTE.APELLIDO, s.FECHA.ToString("dd/MM/yyyy")) }), "ID", "NOMBRE");
                }
            }
            else
            {
                int id = Convert.ToInt32(cookieName.Values["id"]);
                if (recibo != null && recibo.Count != 0)
                {
                    List<CITA> ci = db.CITAs.SqlQuery("select * from CITA where ID_DOCTOR = " + id).ToList();
                    List<CITA> arr = new List<CITA>();
                    foreach (CITA cc in ci)
                    {
                        bool vali = false;
                        foreach (RECIBO re in recibo)
                        {
                            if (re.ID_CITA == cc.ID)
                            {
                                vali = true;
                            }
                        }
                        if (vali == false)
                        {
                            arr.Add(cc);
                        }
                    }

                    ViewBag.ID_CITA = new SelectList(arr.Select(s => new { ID = s.ID, NOMBRE = string.Format("{0} {1} {2}", s.PACIENTE.NOMBRE, s.PACIENTE.APELLIDO, s.FECHA.ToString("dd/MM/yyyy")) }), "ID", "NOMBRE");
                }
                else
                {
                    ViewBag.ID_CITA = new SelectList(db.CITAs.SqlQuery("select * from CITA where ID_DOCTOR = " + id).ToList().Select(s => new { ID = s.ID, NOMBRE = string.Format("{0} {1} {2}", s.PACIENTE.NOMBRE, s.PACIENTE.APELLIDO, s.FECHA.ToString("dd/MM/yyyy")) }), "ID", "NOMBRE");
                }
            }
            PagoViewModel model = new PagoViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PagoViewModel model)
        {
            try
            {
                CITA cita = db.CITAs.Find(model.ID_CITA);
                cita.TIPO_COBRO = model.id_tipocobro;
                cita.FECHA_MODIFICACION = DateTime.Now;
                db.Entry(cita).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                foreach (PACIENTE_ASEGURADORA pa in cita.PACIENTE.PACIENTE_ASEGURADORA)
                {
                    if (pa.ID == model.idAseguradora)
                    {
                        PACIENTE_ASEGURADORA pac = db.PACIENTE_ASEGURADORA.Find(pa.ID);
                        pac.CONSUMIDO = (pac.CONSUMIDO == null? 0 : pac.CONSUMIDO) + model.monto;
                        db.Entry(pac).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                RECIBO recibo = new RECIBO() { ID_ASEGURADORA = model.idAseguradora, ID_CITA = model.ID_CITA, FECHA = DateTime.Now, ESTATUS = "ENVIADO", MONTO = model.monto };
                db.RECIBOes.Add(recibo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                model.messageError = "Ocurrio un error inesperado";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerCita(long id)
        {
            PagoViewModel model = new PagoViewModel();

            if (id != 0)
            {
                model.cita = new CitaViewModel(db.CITAs.Find(id));
                PACIENTE pa = db.PACIENTEs.Find(model.cita.ID_PACIENTE);
                if (pa.PACIENTE_ASEGURADORA != null)
                {
                    model.aseguradora = new List<PacienteAse>();
                    foreach(PACIENTE_ASEGURADORA pas in pa.PACIENTE_ASEGURADORA)
                    {
                        model.aseguradora.Add(new PacienteAse() { id = pas.ID, nombre = pas.ASEGURADORA.NOMBRE });
                    }
                }
            }
            else
            {
                model.messageError = "Ocurrio un error inesperado.";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerCobertura(long idPaciente)
        {
            PACIENTE_ASEGURADORA aseguradora = db.PACIENTE_ASEGURADORA.SqlQuery("select * from PACIENTE_ASEGURADORA where ID_PACIENTE = " + idPaciente).SingleOrDefault();
            decimal valorReal = aseguradora.COBERTURA - (aseguradora.CONSUMIDO == null? Convert.ToDecimal("0.00"): Convert.ToDecimal(aseguradora.CONSUMIDO))  ;
            return Json(valorReal,JsonRequestBehavior.AllowGet);
        }
    }
}