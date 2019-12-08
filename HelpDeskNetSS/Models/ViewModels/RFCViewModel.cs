using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskNetSS.Models.ViewModels
{
    public class RFCViewModel
    {
        public int RFC { get; set; }

        public string IDUsuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripción { get; set; }

        [Display(Name = "Proposito")]
        [DataType(DataType.MultilineText)]
        public string Proposito { get; set; }

        [StringLength(50)]
        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; }

        [StringLength(50)]
        [Display(Name = "Tiempo")]
        public string TiempoEstimado { get; set; }

        [StringLength(50)]
        public string Estatus { get; set; }
    }
}