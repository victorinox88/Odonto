using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace odonto.Models
{
    public class ModuloViewModel:BaseViewModel
    {
        public ModuloViewModel() { }

        public ModuloViewModel(MODULO model)
        {
            ID = model.ID;
            NOMBRE = model.NOMBRE;
            DESCRIPCION = model.DESCRIPCION;
            ESTATUS = model.ESTATUS;
            FECHA_MODIFICACION = model.FECHA_MODIFICACION.ToString();
        }

        public long ID { get; set; }
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }
        [DisplayName("Descripción")]
        public string DESCRIPCION { get; set; }
        [DisplayName("Estatus")]
        public bool ESTATUS { get; set; }
        [DisplayName("Fecha Modificación")]
        public string FECHA_MODIFICACION { get; set; }
        public List<MODULO> listaModulo { get; set; }
    }
}