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
    
    public partial class PAGO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAGO()
        {
            this.PAGO_TIPO = new HashSet<PAGO_TIPO>();
        }
    
        public long ID { get; set; }
        public long ID_RECIBO { get; set; }
        public decimal MONTO { get; set; }
        public System.DateTime FECHA { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual RECIBO RECIBO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAGO_TIPO> PAGO_TIPO { get; set; }
    }
}
