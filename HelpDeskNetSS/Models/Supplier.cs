//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelpDeskNetSS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        public int IDProveedor { get; set; }
        public int IDComponente { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Contacto { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    
        public virtual Component Component { get; set; }
    }
}
