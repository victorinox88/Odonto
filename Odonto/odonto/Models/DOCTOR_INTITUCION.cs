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
    
    public partial class DOCTOR_INTITUCION
    {
        public long ID { get; set; }
        public long ID_DOCTOR { get; set; }
        public long ID_INSTITUCION { get; set; }
    
        public virtual DOCTOR DOCTOR { get; set; }
        public virtual INSTITUCION INSTITUCION { get; set; }
    }
}
