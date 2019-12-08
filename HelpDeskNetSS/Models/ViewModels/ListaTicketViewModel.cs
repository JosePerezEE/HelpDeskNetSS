using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskNetSS.Models.ViewModels
{
    public class ListaTicketViewModel
    {
        public int IDTicket { get; set; }
        public string IDUsuario { get; set; }
        public DateTime? Fecha { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripción { get; set; }
        public int? IDComponente { get; set; }
        public string Prioridad { get; set; }
        public string Estado { get; set; }
        public string Asignacion { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string NombreMantenimiento { get; set; }
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string DescripcionManten { get; set; }

        [Display(Name = "Resolución")]
        [DataType(DataType.MultilineText)]
        public string Resolucion { get; set; }
    }
}