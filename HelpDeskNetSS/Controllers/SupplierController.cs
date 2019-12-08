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
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            List<SupplierViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Suppliers
                        select new SupplierViewModel
                        {
                            IDProveedor = d.IDProveedor,
                            IDComponente = d.IDComponente,
                            Nombre = d.Nombre,
                            Descripción = d.Descripcion,
                            Tipo = d.Tipo,
                            Contacto = d.Contacto,
                            FechaContrato = d.Fecha
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
            SupplierViewModel model = new SupplierViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(SupplierViewModel model)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                if (ModelState.IsValid)
                {
                    HelpDeskEntities db = new HelpDeskEntities();
                    db.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                    var result = db.SP_Ticket_Proveedor(model.IDComponente,model.Nombre, model.Descripción, model.Tipo,model.Contacto,model.FechaContrato);
                    TempData["message"] = "Proveedor agregado exitosamente";
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
        public ActionResult Editar(int id)
        {
            SupplierViewModel model = new SupplierViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Suppliers.Find(id);
                model.IDProveedor = tabla.IDProveedor;
                model.IDComponente = tabla.IDComponente;
                model.Nombre = tabla.Nombre;
                model.Descripción = tabla.Descripcion;
                model.Tipo = tabla.Tipo;
                model.Contacto = tabla.Contacto;
                model.FechaContrato = tabla.Fecha;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(SupplierViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HelpDeskEntities db = new HelpDeskEntities())
                    {
                        var tabla = db.Suppliers.Find(model.IDProveedor);
                        tabla.IDProveedor = model.IDProveedor;
                        tabla.IDComponente = model.IDComponente;
                        tabla.Nombre = model.Nombre;
                        tabla.Descripcion = model.Descripción;
                        tabla.Tipo = model.Tipo;
                        tabla.Contacto = model.Contacto;
                        tabla.Fecha = model.FechaContrato;
                       
                        db.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    TempData["message"] = "La informacion del proveedor ha sido editada exitosamente, proceda a verificar los cambios";
                    return Redirect("~/Supplier/");
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
            SupplierViewModel model = new SupplierViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Suppliers.Find(id);
                model.IDProveedor = tabla.IDProveedor;
                model.IDComponente = tabla.IDComponente;
                model.Nombre = tabla.Nombre;
                model.Descripción = tabla.Descripcion;
                model.Tipo = tabla.Tipo;
                model.Contacto = tabla.Contacto;
                model.FechaContrato = tabla.Fecha;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            SupplierViewModel model = new SupplierViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Suppliers.Find(id);
                db.Suppliers.Remove(tabla);
                db.SaveChanges();

            }
            return Redirect("~/Supplier/");
        }
    }
}