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
    
    public partial class RFC
    {
        public int IDRFC { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string IDUsuario { get; set; }
        public string Descripcion { get; set; }
        public string Proposito { get; set; }
        public string Prioridad { get; set; }
        public string TiempoEstimado { get; set; }
        public string Estatus { get; set; }
    }
}
