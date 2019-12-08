using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskNetSS.Models.ViewModels
{
    public class AccessViewModel
    {
        public int IDAcceso { get; set; }

        public string IDUsuario { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre de usuario")]
        public string Nombre { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        public int? IDComponente { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripción { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }
    }
}