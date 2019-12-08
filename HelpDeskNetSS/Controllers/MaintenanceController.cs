using HelpDeskNetSS.Models;
using HelpDeskNetSS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskNetSS.Controllers
{
    public class MaintenanceController : Controller
    {
        // GET: Maintenance
        public ActionResult Index()
        {
            List<MaintenanceViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Maintenances
                        select new MaintenanceViewModel
                        {
                            IDMantenimiento = d.IDMantenimiento,
                            IDTicket = d.IDTicket,
                            IDUsuario = d.IDUsuario,
                            Asignacion = d.IDUsuarioAsignado,
                            IDComponente = d.IDComponente,
                            NombreMantenimiento = d.Asignacion,
                            DescripcionManten = d.Descripcion,
                            Hora = d.HoraEntrada,
                            Estado = d.Estado
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult Detalles(int id)
        {

            MaintenanceViewModel model = new MaintenanceViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Maintenances.Find(id);
                model.IDMantenimiento = tabla.IDMantenimiento;
                model.IDTicket = tabla.IDTicket;
                model.IDUsuario = tabla.IDUsuario;
                model.Asignacion = tabla.IDUsuarioAsignado;
                model.IDComponente = tabla.IDComponente;
                model.NombreMantenimiento = tabla.Asignacion;
                model.DescripcionManten = tabla.Descripcion;
                model.Hora = tabla.HoraEntrada;
                model.Estado = tabla.Estado;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Detalles(MaintenanceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Manten_Estado(model.IDMantenimiento, model.IDUsuario, model.Asignacion);
                    TempData["message"] = "Mantenimiento registrado y archivado con exito.";
                    return Redirect("~/Maintenance/");
                }
            }
            catch (Exception ex)
            {
                string alerta = "";
                if (ex.InnerException != null)
                {
                    alerta = ex.InnerException.Message;
                }
                else
                {
                    alerta = ex.Message;
                }
                if ((alerta.StartsWith("sp|")) || (alerta.StartsWith("in|")))
                {
                    alerta = alerta.Substring(3);
                }
                else
                {
                    alerta = "Error desconocido";
                }
                TempData["alert"] = alerta;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            MaintenanceViewModel model = new MaintenanceViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Maintenances.Find(id);
                db.Maintenances.Remove(tabla);
                db.SaveChanges();
            }
            return Redirect("~/Maintenance/");
        }
    }
}