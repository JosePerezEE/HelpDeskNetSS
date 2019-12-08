using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskNetSS.Models.ViewModels
{
    public class NuevoComponentViewModel
    {
        public int IDComponente { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        [StringLength(50)]
        [Display(Name = "Localización")]
        public string Localizacion { get; set; }
        public int Seguridad { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Instalación")]
        public DateTime? FechaInstalacion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Último cambio")]
        public DateTime? UltimoCambioFecha { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Caducidad")]
        public DateTime? CaducidadFecha { get; set; }
    }
}