using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace odonto.Models
{
    public class EnfermedadViewModel: BaseViewModel
    {
        public EnfermedadViewModel(){}

        public EnfermedadViewModel(ENFERMEDAD model)
        {
            this.ID = model.ID;
            this.NOMBRE = model.NOMBRE;
            this.DESCRIPCION = model.DESCRIPCION;
            this.TIPO = model.TIPO.Trim();
            this.FECHA_MODIFICACION = model.FECHA_MODIFICACION;
        }

        public long ID { get; set; }
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }
        [DisplayName("Descripción")]
        public string DESCRIPCION { get; set; }
        [DisplayName("Tipo de Enfermedad")]
        public string TIPO { get; set; }
        [DisplayName("Fecha de Modificación")]
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
        public List<ENFERMEDAD> listaEnfermedad { get; set; }
    }
}