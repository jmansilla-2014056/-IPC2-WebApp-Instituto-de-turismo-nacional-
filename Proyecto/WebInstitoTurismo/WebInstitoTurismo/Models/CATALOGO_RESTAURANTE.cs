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
    
    public partial class CATALOGO_RESTAURANTE
    {
        public int ID { get; set; }
        public Nullable<bool> VALIDACION { get; set; }
        public Nullable<int> ID_RESTAURANTE { get; set; }
        public Nullable<int> ID_ESPECIALIDAD { get; set; }
    
        public virtual ESPECIALIDAD ESPECIALIDAD { get; set; }
        public virtual EMPRESA_RESTAURANTE EMPRESA_RESTAURANTE { get; set; }
    }
}
