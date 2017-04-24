using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace odonto.Models
{
    public class ReporteViewModel: BaseViewModel
    {
        private SelectList _listaSeguro;

        public long seguro { get; set; }
        public string mes { get; set; }
        [DisplayName("Paciente")]
        public long idPaciente { get; set; }
        [DisplayName("Seguro")]
        public long idSeguro { get; set; }
        [DisplayName("Tratamiento")]
        public long idTrata { get; set; }
        [DisplayName("Reportes")]
        public long idReporte { get; set; }
        [DisplayName("Fecha de Inicio")]
        public string fechaInicio { get; set; }
        [DisplayName("Fecha de Fin")]
        public string fechaFin { get; set; }
        public string[] lista = new string[9] { "Citas por Rangos de Fechas", "Tratamientos por Pacientes", "Pacientes por Aseguradora", "Pagos Realizados por Tratamientos", "Contratos por Pacientes", "Contratos por Aseguradora", "Pagos Realizados por Rango de Fechas", "Enfermedades mas Frecuentes", "Estatus de Pago de Aseguradoras"};

        public SelectList listaReporte
        {
            get
            {
                return new SelectList(CargarReportes(), "Id", "Nombre");
            }
            set
            {
                this._listaSeguro = value;
            }
        }

       

        public List<Reporte> CargarReportes()
        {
            List<Reporte> reporte = new List<Reporte>();
            for (int i=0; i <= 8; i++)
            {
                reporte.Add(new Reporte() { Id = i, Nombre = lista[i] });
            }


            return reporte;
        }

    }
}