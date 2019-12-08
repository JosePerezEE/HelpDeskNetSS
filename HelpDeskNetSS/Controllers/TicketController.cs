using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpDeskNetSS.Models.ViewModels;
using HelpDeskNetSS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpDeskNetSS.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            List<ListaTicketViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Tickets
                        select new ListaTicketViewModel
                        {
                            IDTicket = d.IDTicket,
                            IDUsuario = d.IDUsuario,
                            Fecha = d.Fecha,
                            Nombre = d.Nombre,
                            Descripción = d.Descripcion,
                            IDComponente = d.IDComponente,
                            Prioridad = d.Prioridad,
                            Estado = d.Estado,
                            Asignacion = d.IDUsuarioAssignee
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult IndexTicket()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<ListaTicketViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Tickets
                        where d.IDUsuario == user.Id
                        select new ListaTicketViewModel
                        {
                            IDTicket = d.IDTicket,
                            IDUsuario = d.IDUsuario,
                            Fecha = d.Fecha,
                            Nombre = d.Nombre,
                            Descripción = d.Descripcion,
                            IDComponente = d.IDComponente,
                            Prioridad = d.Prioridad,
                            Estado = d.Estado,
                            Asignacion = d.IDUsuarioAssignee
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
                    Text = d.Nombre.ToString() +" / "+ d.Localizacion.ToString(),
                    Value = d.IDComponente.ToString(),
                    Selected = false
                };
            });
            ViewBag.lista = lista;

            TicketViewModel model = new TicketViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(TicketViewModel model)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Registro(User.Identity.GetUserId(), model.Nombre, model.Descripción, model.IDComponente);
                    TempData["message"] = "Verifique su correo para revisar la confirmación del registro de su ticket";
                    return Redirect("~/Home/Index");
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

        public ActionResult NuevoMantenimiento(int id)
        {
            MaintenanceViewModel model = new MaintenanceViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Tickets.Find(id);
                model.IDTicket = tabla.IDTicket;
                model.IDUsuario = tabla.IDUsuario;
                model.IDComponente = tabla.IDComponente;
                model.Asignacion = tabla.IDUsuarioAssignee;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult NuevoMantenimiento(MaintenanceViewModel model)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Mantenimiento(model.IDTicket, model.IDUsuario, model.Asignacion, model.IDComponente,model.NombreMantenimiento,model.DescripcionManten);
                    TempData["message"] = "Mantenimiento exitosamente registrado. Usuario notificado.";
                    return Redirect("~/Ticket/");
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
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = roleManager.FindByName("SuperAdmin").Users.First();
            var usersInRole = context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();

            List<SelectListItem> lista = usersInRole.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.UserName,
                    Value = d.UserName,
                    Selected = false
                };
            });
            ViewBag.lista = lista;

            ListaTicketViewModel model = new ListaTicketViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Tickets.Find(id);
                model.IDTicket = tabla.IDTicket;
                model.IDUsuario = tabla.IDUsuario;
                model.Fecha = tabla.Fecha;
                model.Nombre = tabla.Nombre;
                model.Descripción = tabla.Descripcion;
                model.IDComponente = tabla.IDComponente;
                model.Prioridad = tabla.Prioridad;
                model.Estado = tabla.Estado;
                model.Asignacion = tabla.IDUsuarioAssignee;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(ListaTicketViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        HelpDeskEntities db = new HelpDeskEntities();
                        db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                        var result = db.SP_Ticket_SEGUIMIENTO(model.IDTicket, model.IDUsuario, model.Prioridad, model.Estado,model.Asignacion);
                        TempData["message"] = "Seguimiento de ticket realizado correctamente";
                        return Redirect("~/Ticket/");
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
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = roleManager.FindByName("SuperAdmin").Users.First();
            var usersInRole = context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();

            List<SelectListItem> lista = usersInRole.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.UserName,
                    Value = d.UserName,
                    Selected = false
                };
            });
            ViewBag.lista = lista;
            return View(model);
        }

        [HttpGet]
        public ActionResult Detalles(int id)
        {
            ListaTicketViewModel model = new ListaTicketViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Tickets.Find(id);
                model.IDTicket = tabla.IDTicket;
                model.IDUsuario = tabla.IDUsuario;
                model.Fecha = tabla.Fecha;
                model.Nombre = tabla.Nombre;
                model.Descripción = tabla.Descripcion;
                model.IDComponente = tabla.IDComponente;
                model.Prioridad = tabla.Prioridad;
                model.Estado = tabla.Estado;
                model.Asignacion = tabla.IDUsuarioAssignee;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Detalles(ListaTicketViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_RevisarProveedor(model.IDTicket, model.IDUsuario);
                    TempData["message"] = "Seguimiento de ticket a proveedores realizado exitosamente";
                    return Redirect("~/Ticket/");
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
        public ActionResult Resolucion(int id)
        {
            ListaTicketViewModel model = new ListaTicketViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Tickets.Find(id);
                model.IDTicket = tabla.IDTicket;
                model.IDUsuario = tabla.IDUsuario;
                model.Fecha = tabla.Fecha;
                model.Nombre = tabla.Nombre;
                model.Descripción = tabla.Descripcion;
                model.IDComponente = tabla.IDComponente;
                model.Prioridad = tabla.Prioridad;
                model.Estado = tabla.Estado;
                model.Asignacion = tabla.IDUsuarioAssignee;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Resolucion(ListaTicketViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Respuesta(model.IDTicket, model.Resolucion);
                    TempData["message"] = "Respuesta al Ticket exitosa";
                    return Redirect("~/Ticket/Index");
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
            ListaTicketViewModel model = new ListaTicketViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Tickets.Find(id);
                db.Tickets.Remove(tabla);
                db.SaveChanges();
            }
            return Redirect("~/Ticket/");
        }


    }
}