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
    
    public partial class TELEFONO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TELEFONO()
        {
            this.RELACION_TELEFONO = new HashSet<RELACION_TELEFONO>();
        }
    
        public long ID { get; set; }
        public string NUMERO { get; set; }
        public string TIPOCHECK { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RELACION_TELEFONO> RELACION_TELEFONO { get; set; }
    }
}