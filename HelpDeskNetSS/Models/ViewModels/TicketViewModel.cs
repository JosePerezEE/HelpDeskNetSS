using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskNetSS.Models.ViewModels
{
    public class TicketViewModel
    {
        public string IDUsuario { get; set; }
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripción { get; set; }
        public int IDComponente { get; set; }
    }
}