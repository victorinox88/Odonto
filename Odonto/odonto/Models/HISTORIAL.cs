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
    
    public partial class HISTORIAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HISTORIAL()
        {
            this.DETALLE_HISTORIAL = new HashSet<DETALLE_HISTORIAL>();
        }
    
        public long ID { get; set; }
        public long ID_PACIENTE { get; set; }
        public Nullable<long> ID_CITA { get; set; }
        public string DIENTE { get; set; }
        public string SVG_DOCTOR { get; set; }
        public string SVG_PACIENTE { get; set; }
        public string SVG_MODAL { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual CITA CITA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_HISTORIAL> DETALLE_HISTORIAL { get; set; }
        public virtual PACIENTE PACIENTE { get; set; }
    }
}
