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
    using System.ComponentModel.DataAnnotations;

    public partial class FOTO_SITIO
    {
        public int ID { get; set; }
        public byte[] DIRECCION { get; set; }
        public string TITULO { get; set; }
        public Nullable<int> ID_SITIO { get; set; }
    
        public virtual SITIO_TURISTICO SITIO_TURISTICO { get; set; }
    }
}
