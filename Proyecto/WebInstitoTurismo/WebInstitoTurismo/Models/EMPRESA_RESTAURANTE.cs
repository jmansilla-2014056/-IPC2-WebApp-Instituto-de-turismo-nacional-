//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebInstitoTurismo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EMPRESA_RESTAURANTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPRESA_RESTAURANTE()
        {
            this.CATALOGO_RESTAURANTE = new HashSet<CATALOGO_RESTAURANTE>();
        }
    
        public int ID { get; set; }
        public Nullable<System.TimeSpan> HORA_I { get; set; }
        public Nullable<System.TimeSpan> HORA_F { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATALOGO_RESTAURANTE> CATALOGO_RESTAURANTE { get; set; }
        public virtual EMPRESA EMPRESA { get; set; }
    }
}