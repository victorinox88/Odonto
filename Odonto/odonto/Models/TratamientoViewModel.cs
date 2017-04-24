using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace odonto.Models
{
    public class TratamientoViewModel: BaseViewModel
    {
        private SelectList _listaDIENTE;

        
        public long ID { get; set; }
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }
        [DisplayName("Descripcion")]
        public string DESCRIPCION { get; set; }
        [DisplayName("Estatus")]
        public bool ESTATUS { get; set; }
        [DisplayName("Fecha de Modificacion")]
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
        [DisplayName("Lista de DIENTE")]
        public int diente { get; set; }
        [DisplayName("Color")]
        public string color { get; set; }
        [DisplayName("Monto")]
        public Nullable<decimal> MONTO { get; set; }
        public List<TratamientoViewModel> listaTratamiento { get; set; }
        public List<TRATA_DIENTE> listaTraDien { get; set; }
        public SelectList listaDIENTE
        {
            get
            {
                return new SelectList(CargarDIENTE(), "Id", "Nombre");
            }
            set
            {
                this._listaDIENTE = value;
            }
        }

        public ICollection<DIENTE> CargarDIENTE()
        {
            NEOODONTOEntities db = new NEOODONTOEntities();
            List<DIENTE> lista = db.DIENTEs.ToList();
            return lista;
        }
    }
}