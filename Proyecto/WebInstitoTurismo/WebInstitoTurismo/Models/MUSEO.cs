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
    
    public partial class MUSEO
    {
        public int ID { get; set; }
        public Nullable<System.TimeSpan> HORA_INICIO { get; set; }
        public Nullable<System.TimeSpan> HORA_FIN { get; set; }
        public string TARIFA { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
    
        public virtual EMPRESA EMPRESA { get; set; }
    }
}
