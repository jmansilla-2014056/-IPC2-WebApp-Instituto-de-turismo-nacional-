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
    
    public partial class RECORRIDO_EMPRESA
    {
        public int ID { get; set; }
        public Nullable<int> ID_RECORRIDO { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
    
        public virtual EMPRESA EMPRESA { get; set; }
        public virtual RECORRIDO RECORRIDO { get; set; }
    }
}
