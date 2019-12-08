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
    public class ComponentController : Controller
    {
        // GET: Component
        public ActionResult Index()
        {
            List<NuevoComponentViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Components
                        select new NuevoComponentViewModel
                        {
                            IDComponente = d.IDComponente,
                            Nombre = d.Nombre,
                            Descripcion = d.Descripcion,
                            Estado = d.Estado,
                            Localizacion = d.Localizacion,
                            Seguridad = d.Seguridad,
                            FechaInstalacion = d.FechaInstalacion,
                            UltimoCambioFecha = d.UltimoCambio,
                            CaducidadFecha = d.Caducidad
                            
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult Nuevo()
        {
            NuevoComponentViewModel model = new NuevoComponentViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(NuevoComponentViewModel model)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Componente(model.Nombre, model.Descripcion, model.Estado, model.Localizacion, model.Seguridad,model.FechaInstalacion,model.UltimoCambioFecha,model.CaducidadFecha);
                    TempData["message"] = "Componente agregado exitosamente";
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
            return View(model);
        }
        public ActionResult Editar(int id)
        {
            NuevoComponentViewModel model = new NuevoComponentViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Components.Find(id);
                model.IDComponente = tabla.IDComponente;
                model.Nombre = tabla.Nombre;
                model.Descripcion = tabla.Descripcion;
                model.Localizacion = tabla.Localizacion;
                model.Estado = tabla.Estado;
                model.Seguridad = tabla.Seguridad;
                model.FechaInstalacion = tabla.FechaInstalacion;
                model.UltimoCambioFecha = tabla.UltimoCambio;
                model.CaducidadFecha = tabla.Caducidad;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(NuevoComponentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HelpDeskEntities db = new HelpDeskEntities())
                    {
                        var tabla = db.Components.Find(model.IDComponente);
                        tabla.IDComponente = model.IDComponente;
                        tabla.Nombre = model.Nombre;
                        tabla.Descripcion = model.Descripcion;
                        tabla.Estado = model.Estado;
                        tabla.Localizacion = model.Localizacion;
                        tabla.Seguridad = model.Seguridad;
                        tabla.FechaInstalacion = model.FechaInstalacion;
                        tabla.UltimoCambio = model.UltimoCambioFecha;
                        tabla.Caducidad = model.CaducidadFecha;

                        db.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    TempData["message"] = "La informacion del equipo o componente ha sido editada exitsamente, proceda a verificar los cambios";
                    return Redirect("~/Component/");
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
                TempData["alert"] = alerta;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Detalles(int id)
        {
            NuevoComponentViewModel model = new NuevoComponentViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Components.Find(id);
                model.IDComponente = tabla.IDComponente;
                model.Nombre = tabla.Nombre;
                model.Descripcion = tabla.Descripcion;
                model.Localizacion = tabla.Localizacion;
                model.Estado = tabla.Estado;
                model.Seguridad = tabla.Seguridad;
                model.FechaInstalacion = tabla.FechaInstalacion;
                model.UltimoCambioFecha = tabla.UltimoCambio;
                model.CaducidadFecha = tabla.Caducidad;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            NuevoComponentViewModel model = new NuevoComponentViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Components.Find(id);
                db.Components.Remove(tabla);
                db.SaveChanges();

            }
            return Redirect("~/Component/");
        }
    }
}