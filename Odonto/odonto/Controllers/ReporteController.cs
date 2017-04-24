using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using odonto.Models;
using System.Globalization;

namespace odonto.Controllers
{
    public class ReporteController : BaseController
    {
       
        NEOODONTOEntities db = new NEOODONTOEntities();
        // GET: Reporte
        public ActionResult Index()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            ReporteViewModel model = new ReporteViewModel();
            return View(model);
        }

        public ActionResult ObtenerCitaPorRango(string fechaInicio, string fechaFin)
        {
            DateTime fechaIni = Convert.ToDateTime(DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
            DateTime fechaF = Convert.ToDateTime(DateTime.ParseExact(fechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
            string query = "select * from CITA where FECHA >= " + fechaIni + " and FECHA <= " + fechaF;
            List<CITA> lista = db.CITAs.SqlQuery("select * from CITA where FECHA >= '" + fechaIni + "' and FECHA <= '" + fechaF +"'").ToList<CITA>();
            return PartialView("CitasPorRango",lista);
        }

        public ActionResult ObtenerTratamientoPaciente(long id)
        {
            PACIENTE pa = db.PACIENTEs.Find(id);
            return PartialView("TratamientosPorPaciente", pa);
        }

        public ActionResult ObtenerPacienteAseguradora(long id)
        {
            List<PACIENTE> lista = db.PACIENTEs.SqlQuery("select p.* from PACIENTE p, PACIENTE_ASEGURADORA pa where pa.ID_PACIENTE = p.ID and pa.ID_ASEGURADORA =" + id).ToList<PACIENTE>();
            return PartialView("PacientesPorAseguradora", lista);
        }

        public ActionResult ContratoPorAseguradora(long id)
        {
            List<DOCTOR> lista = db.DOCTORs.SqlQuery("select distinct d.* from DOCTOR d, CONTRATO c, ASEGURADORA a where c.ID_ASEGURADORA = a.ID and c.ID_DOCTOR = d.ID and a.ID = " + id).ToList<DOCTOR>();
            return PartialView("ContratosPorAseguradora", lista);
        }

        public ActionResult EstatusPagoAseguradora(long id)
        {
            List<RECIBO> lista = db.RECIBOes.SqlQuery("select r.* from RECIBO r, PACIENTE_ASEGURADORA pa where r.ID_ASEGURADORA = pa.ID and pa.ID_ASEGURADORA =" + id).ToList<RECIBO>();
            return PartialView("EstatusPagoPorAseguradora", lista);
        }

    }
}