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
    
    public partial class DETALLE_HISTORIAL
    {
        public long ID { get; set; }
        public long ID_TRATA_DIENTE { get; set; }
        public long ID_HISTORIAL { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> POSICION { get; set; }
        public System.DateTime FECHA { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual HISTORIAL HISTORIAL { get; set; }
        public virtual TRATA_DIENTE TRATA_DIENTE { get; set; }
    }
}