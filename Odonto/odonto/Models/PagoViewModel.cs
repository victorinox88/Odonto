using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace odonto.Models
{
    public class PagoViewModel: BaseViewModel
    {
        private SelectList _TipoCobro;

        public long ID_CITA { get; set; }

        public CitaViewModel cita { get; set; }

        public DETALLE_HISTORIAL detalle { get; set; }

        public SelectList TipoCobro
        {
            get
            {
                return new SelectList(CargarCobro(), "id", "nombre");
            }
            set
            {
                this._TipoCobro = value;
            }
        }

        public List<TipoCobro> CargarCobro()
        {
            return new List<Models.TipoCobro>() { new Models.TipoCobro() { id = "Seguro", nombre = "Seguro" }, new Models.TipoCobro() { id = "Paciente", nombre = "Paciente" } };
        }

        public string id_tipocobro { get; set; }

        public long idTipoTarjeta { get; set; }

        [DisplayName("Tipo de Pago")]
        public long IdTipoPago { get; set; }

        public List<PacienteAse> aseguradora { get; set; }

        public long idAseguradora { get; set; }
        public decimal monto { get; set; }
        [DisplayName("Monto Total")]
        public decimal montoTotal { get; set; }

        [DisplayName("Monto a Pagar")]
        public decimal montoPagado { get; set; }

        [DisplayName("Monto a Restante")]
        public decimal montoRestante { get; set; }

        [DisplayName("Numero de Comprobante")]
        public string numeroComprobante { get; set; }

        [DisplayName("Numero de Cheque")]
        public string numeroCheque { get; set; }

        [DisplayName("Cobertura")]
        public decimal cobertura { get; set; }

        public SelectList listaTipoTarjeta
        {
            get
            {
                List<TipoTarjeta> tipot = new List<TipoTarjeta>();
                tipot.Add(new TipoTarjeta() { id = 1, nombre = "MasterCard" });
                tipot.Add(new TipoTarjeta() { id = 1, nombre = "Visa" });
                
                return new SelectList(tipot, "id", "nombre");
            }
        }

    }

    public class TipoCobro
    {
        public string id { get; set; }

        public string nombre { get; set; }
    }

    public class TipoTarjeta
    {
        public int id { get; set; }

        public string nombre { get; set; }
    }

    public class PacienteAse
    {

        public long id { get; set; }

        public string nombre { get; set; }
    }
}