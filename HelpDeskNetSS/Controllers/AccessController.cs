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
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            List<AccessViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Accesses
                        select new AccessViewModel
                        {
                            IDAcceso = d.IDAcceso,
                            IDUsuario = d.IDUsuario,
                            Nombre = d.Nombre,
                            Descripción = d.Descripcion,
                            IDComponente = d.IDComponente,
                            Tipo = d.Tipo,
                            Fecha = d.Fecha,
                            Estado = d.Estado
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult IndexTicket()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<AccessViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Accesses
                        where d.IDUsuario == user.Id
                        select new AccessViewModel
                        {
                            IDAcceso = d.IDAcceso,
                            IDUsuario = d.IDUsuario,
                            Nombre = d.Nombre,
                            Descripción = d.Descripcion,
                            IDComponente = d.IDComponente,
                            Tipo = d.Tipo,
                            Fecha = d.Fecha,
                            Estado = d.Estado
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult Nuevo()
        {
            List<ComponentViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Components
                        select new ComponentViewModel
                        {
                            Localizacion = d.Localizacion,
                            IDComponente = d.IDComponente,
                            Nombre = d.Nombre
                        }).ToList();
            }

            List<SelectListItem> lista = list.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString() + " / " + d.Localizacion.ToString(),
                    Value = d.IDComponente.ToString(),
                    Selected = false
                };
            });
            ViewBag.lista = lista;
            AccessViewModel model = new AccessViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(AccessViewModel model)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Acceso(User.Identity.GetUserId(),model.IDComponente, model.Descripción, model.Tipo);
                    TempData["message"] = "Petición de acceso exitoso. Encargado de la gestión fue notificado. Revisar correo.";
                    return Redirect("~/Access/IndexTicket");
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
            List<ComponentViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Components
                        select new ComponentViewModel
                        {
                            Localizacion = d.Localizacion,
                            IDComponente = d.IDComponente,
                            Nombre = d.Nombre
                        }).ToList();
            }

            List<SelectListItem> lista = list.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString() + " / " + d.Localizacion.ToString(),
                    Value = d.IDComponente.ToString(),
                    Selected = false
                };
            });
            ViewBag.lista = lista;
            return View(model);
        }

        public ActionResult Editar(int id)
        {

            AccessViewModel model = new AccessViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Accesses.Find(id);
                model.IDAcceso = tabla.IDAcceso;
                model.IDUsuario = tabla.IDUsuario;
                model.Nombre = tabla.Nombre;
                model.Descripción = tabla.Descripcion;
                model.IDComponente = tabla.IDComponente;
                model.Tipo = tabla.Tipo;
                model.Fecha = tabla.Fecha;
                model.Estado = tabla.Estado;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(AccessViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Acceso_Aceptacion(model.IDAcceso, model.IDUsuario, model.Estado);
                    TempData["message"] = "Decision enviada al usuario solicitante";
                    return Redirect("~/Access/");
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
            AccessViewModel model = new AccessViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Accesses.Find(id);
                db.Accesses.Remove(tabla);
                db.SaveChanges();
            }
            return Redirect("~/Access/");
        }
    }
}