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
    
    public partial class ASEGURADORA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASEGURADORA()
        {
            this.PACIENTE_ASEGURADORA = new HashSet<PACIENTE_ASEGURADORA>();
            this.CONTRATOes = new HashSet<CONTRATO>();
        }
    
        public long ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACIENTE_ASEGURADORA> PACIENTE_ASEGURADORA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO> CONTRATOes { get; set; }
    }
}