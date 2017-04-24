using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace odonto.Models
{
    public class ContratoViewModel
    {
        public ContratoViewModel(){}

        public ContratoViewModel(CONTRATO model)
        {
            ID = model.ID;
            ID_DOCTOR = model.ID_DOCTOR;
            ID_ASEGURADORA = model.ID_ASEGURADORA;
            ID_INSTITUCION = model.ID_INSTITUCION;
            FECHA_INICIO = model.FECHA_INICIO.ToString();
            FECHA_FIN = model.FECHA_FIN.ToString();
            ESTATUS = model.ESTATUS;
            FECHA_MODIFICACION = model.FECHA_MODIFICACION;
        }

        public long ID { get; set; }
        [DisplayName("Doctor")]
        public Nullable<long> ID_DOCTOR { get; set; }
        [DisplayName("Institución")]
        public Nullable<long> ID_INSTITUCION { get; set; }
        [DisplayName("Aseguradora")]
        public long ID_ASEGURADORA { get; set; }
        [DisplayName("Fecha de Inicio")]
        public string FECHA_INICIO { get; set; }
        [DisplayName("Fecha Fin")]
        public string FECHA_FIN { get; set; }
        [DisplayName("Estatus")]
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    }
}