using HelpDeskNetSS.Models;
using HelpDeskNetSS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskNetSS.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        public ActionResult Index()
        {
            List<PolicyViewModel> list = null;
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                list = (from d in db.Policies
                        select new PolicyViewModel
                        {
                            IDPolitica = d.IDPolitica,
                            Descripcion = d.Descripcion,
                            Fecha = d.Fecha,
                            FechaCambio = d.Fecha
                        }).ToList();
            }
            return View(list);
        }

        public ActionResult Editar(int id)
        {

            PolicyViewModel model = new PolicyViewModel();
            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Policies.Find(id);
                model.IDPolitica = tabla.IDPolitica;
                model.Descripcion = tabla.Descripcion;
                model.Fecha = tabla.Fecha;
                model.FechaCambio = tabla.FechaCambio;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(PolicyViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HelpDeskEntities db = new HelpDeskEntities())
                    {
                        var tabla = db.Policies.Find(model.IDPolitica);
                        tabla.IDPolitica = model.IDPolitica;
                        tabla.Descripcion = model.Descripcion;
                        tabla.Fecha = model.Fecha;
                        tabla.FechaCambio = model.FechaCambio;

                        db.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    TempData["message"] = "La informacion de la politica de seguridad ha sido actualizada exitosamente";
                    return Redirect("~/Policy/");
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
        public ActionResult Eliminar(int id)
        {
            AccessViewModel model = new AccessViewModel();

            using (HelpDeskEntities db = new HelpDeskEntities())
            {
                var tabla = db.Policies.Find(id);
                db.Policies.Remove(tabla);
                db.SaveChanges();
            }
            return Redirect("~/Policy/");
        }
    }
}