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
    
    public partial class TRATA_DIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRATA_DIENTE()
        {
            this.DETALLE_HISTORIAL = new HashSet<DETALLE_HISTORIAL>();
        }
    
        public long ID { get; set; }
        public long ID_TRATAMIENTO { get; set; }
        public long ID_DIENTE { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_HISTORIAL> DETALLE_HISTORIAL { get; set; }
        public virtual DIENTE DIENTE { get; set; }
        public virtual TRATAMIENTO TRATAMIENTO { get; set; }
    }
}