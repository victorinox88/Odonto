//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace odonto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETALLE_CONTARTO
    {
        public long ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal MONTO { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
        public long ID_CONTRATO { get; set; }
    
        public virtual CONTRATO CONTRATO { get; set; }
    }
}