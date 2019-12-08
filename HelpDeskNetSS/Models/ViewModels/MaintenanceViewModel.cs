using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskNetSS.Models.ViewModels
{
    public class MaintenanceViewModel
    {
        public int IDMantenimiento { get; set; }
        public int? IDTicket { get; set; }
        public string IDUsuario { get; set; }
        public int? IDComponente { get; set; }
        public string Asignacion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora")]
        public DateTime? Hora { get; set; }
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string NombreMantenimiento { get; set; }
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string DescripcionManten { get; set; }
        public string Estado { get; set; }
    }
}