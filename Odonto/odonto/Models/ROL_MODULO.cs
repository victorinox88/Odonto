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
    
    public partial class ROL_MODULO
    {
        public long ID { get; set; }
        public long ID_ROL { get; set; }
        public long ID_MODULO { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual MODULO MODULO { get; set; }
        public virtual ROL ROL { get; set; }
    }
}