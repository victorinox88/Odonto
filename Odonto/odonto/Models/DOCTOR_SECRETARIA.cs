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
    
    public partial class DOCTOR_SECRETARIA
    {
        public long ID { get; set; }
        public long ID_DOCTOR { get; set; }
        public long ID_SECRETARIA { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual DOCTOR DOCTOR { get; set; }
        public virtual SECRETARIA SECRETARIA { get; set; }
    }
}