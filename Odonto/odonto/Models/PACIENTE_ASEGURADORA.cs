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
    
    public partial class PACIENTE_ASEGURADORA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PACIENTE_ASEGURADORA()
        {
            this.RECIBOes = new HashSet<RECIBO>();
        }
    
        public long ID { get; set; }
        public long ID_PACIENTE { get; set; }
        public long ID_ASEGURADORA { get; set; }
        public decimal COBERTURA { get; set; }
        public Nullable<decimal> CONSUMIDO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual ASEGURADORA ASEGURADORA { get; set; }
        public virtual PACIENTE PACIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECIBO> RECIBOes { get; set; }
    }
}