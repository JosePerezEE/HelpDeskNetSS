using HelpDeskNetSS.Models;
using HelpDeskNetSS.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskNetSS.Controllers
{
    public class ChangeController : Controller
    {
        // GET: Change
        public ActionResult Index()
        {
            List<RFCViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.RFCs
                        select new RFCViewModel
                        {
                            RFC = d.IDRFC,
                            IDUsuario = d.IDUsuario,
                            Fecha = d.Fecha,
                            Descripción = d.Descripcion,
                            Proposito = d.Proposito,
                            Prioridad = d.Prioridad,
                            TiempoEstimado = d.TiempoEstimado,
                            Estatus = d.Estatus
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult IndexTicket()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<RFCViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.RFCs
                        where d.IDUsuario == user.Id
                        select new RFCViewModel
                        {
                            RFC = d.IDRFC,
                            IDUsuario = d.IDUsuario,
                            Fecha = d.Fecha,
                            Descripción = d.Descripcion,
                            Proposito = d.Proposito,
                            Prioridad = d.Prioridad,
                            TiempoEstimado = d.TiempoEstimado,
                            Estatus = d.Estatus
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult Nuevo()
        {
            RFCViewModel model = new RFCViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(RFCViewModel model)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_RFC(User.Identity.GetUserId(), model.Descripción, model.Proposito, model.Prioridad, model.TiempoEstimado);
                    TempData["message"] = "Petición de cambio exitoso. Encargado de la gestión fue notificado. Revisar correo.";
                    return Redirect("~/Change/IndexTicket");
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

        public ActionResult Editar(int id)
        {

            RFCViewModel model = new RFCViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.RFCs.Find(id);
                model.RFC = tabla.IDRFC;
                model.IDUsuario = tabla.IDUsuario;
                model.Fecha = tabla.Fecha;
                model.Descripción = tabla.Descripcion;
                model.Proposito = tabla.Proposito;
                model.Prioridad = tabla.Prioridad;
                model.TiempoEstimado = tabla.TiempoEstimado;
                model.Estatus = tabla.Estatus;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(RFCViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_RFC_Aceptacion(model.RFC, model.IDUsuario, model.Estatus);
                    TempData["message"] = "Decision enviada al usuario solicitante";
                    return Redirect("~/Change/");
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
            RFCViewModel model = new RFCViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.RFCs.Find(id);
                db.RFCs.Remove(tabla);
                db.SaveChanges();
            }
            return Redirect("~/Change/");
        }
    }
}