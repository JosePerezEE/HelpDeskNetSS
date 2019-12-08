using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskNetSS.Models.ViewModels
{
    public class SupplierViewModel
    {
        public int IDProveedor { get; set; }
        public int IDComponente { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripción { get; set; }
        public string Tipo { get; set; }
        public string Contacto { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Contrato")]
        public DateTime? FechaContrato { get; set; }
    }
}