using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace odonto.Models
{
    public class AseguradoraViewModel: BaseViewModel
    {
        public long ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }

        public List<ASEGURADORA> listaAseguradora { get; set; }
    }
}