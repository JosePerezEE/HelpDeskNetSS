using HelpDeskNetSS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskNetSS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            HelpDeskEntities dbs = new HelpDeskEntities();
            if (User.IsInRole("SuperAdmin"))
            {
                int countTickets = (from row in dbs.Tickets
                                    select row).Count();
                ViewBag.Tickets = countTickets;

                int countTicketsSeg = (from row in dbs.Tickets
                                       where row.Estado == "SIN SEGUIMIENTO"
                                       select row).Count();
                ViewBag.TicketsAssignee = countTicketsSeg;

                int countTicketSeg = (from row in dbs.Tickets
                                      where row.Estado != "SIN SEGUIMIENTO"
                                      select row).Count();
                ViewBag.TicketsAssigneeS = countTicketSeg;

                int countTicketUsuario = (from row in dbs.Tickets
                                          where row.IDUsuarioAssignee == user.UserName
                                          select row).Count();
                ViewBag.TicketsUsuario = countTicketUsuario;

                var PorcentajeSSeg = (double)countTicketsSeg / (double)countTickets * 100;
                ViewBag.PorcentajeTicket = PorcentajeSSeg;

                return View();
            }
            if (User.IsInRole("Administrador"))
            {
                int Numcambios = (from row in dbs.RFCs
                                  select row).Count();
                ViewBag.Numcambios = Numcambios;

                int NumcambiosSeg = (from row in dbs.RFCs
                                      where row.Estatus == "EN SEGUIMIENTO"
                                      select row).Count();
                ViewBag.NumcambiosSeg = NumcambiosSeg;

                int NumcambiosAcep = (from row in dbs.RFCs
                                      where row.Estatus == "ACEPTADO"
                                      select row).Count();
                ViewBag.NumcambiosAcep = NumcambiosAcep;

                int NumcambiosRech = (from row in dbs.RFCs
                                      where row.Estatus == "RECHAZADO"
                                      select row).Count();
                ViewBag.NumcambiosRech = NumcambiosRech;

                int Numacceso = (from row in dbs.Accesses
                                  select row).Count();
                ViewBag.Numacceso = Numacceso;

                int NumaccesoSeg = (from row in dbs.Accesses
                                     where row.Estado == "EN SEGUIMIENTO"
                                     select row).Count();
                ViewBag.NumaccesoSeg = NumaccesoSeg;

                int NumaccesoAcep = (from row in dbs.Accesses
                                     where row.Estado == "ACEPTADO"
                                     select row).Count();
                ViewBag.NumaccesoAcep = NumaccesoAcep;

                int NumaccesoRech = (from row in dbs.Accesses
                                     where row.Estado == "RECHAZADO"
                                     select row).Count();
                ViewBag.NumaccesoRech = NumaccesoRech;

                int Seguimiento = (from row in dbs.Tickets
                                       where row.Estado == "EN SEGUIMIENTO"
                                       select row).Count();
                ViewBag.Seguimiento = Seguimiento;

                int Noseguimiento = (from row in dbs.Tickets
                                      where row.Estado == "SIN SEGUIMIENTO"
                                      select row).Count();
                ViewBag.Noseguimiento = Noseguimiento;

                int Proveedor = (from row in dbs.Tickets
                                          where row.Estado == "PROVEEDORES"
                                          select row).Count();
                ViewBag.Proveedor = Proveedor;
                int Mantenimiento = (from row in dbs.Tickets
                                          where row.Estado == "MANTENIMIENTO"
                                          select row).Count();
                ViewBag.Mantenimiento = Mantenimiento;
                int Resuelto = (from row in dbs.Tickets
                                          where row.Estado == "RESUELTO"
                                          select row).Count();
                ViewBag.Resuelto = Resuelto;

                return View();
            }
            if (User.IsInRole("Usuario"))
            {

                int TicketPedido = (from row in dbs.Tickets
                                    where row.IDUsuario == user.Id
                                    select row).Count();
                ViewBag.TicketUsuarioM = TicketPedido;

                int TicketUsuarioSeg = (from row in dbs.Tickets
                                        where row.IDUsuario == user.Id && row.Estado != "SIN SEGUIMIENTO"
                                        select row).Count();
                ViewBag.TicketUsuarioS = TicketUsuarioSeg;

                int TicketUsuarioNSeg = (from row in dbs.Tickets
                                         where row.IDUsuario == user.Id && row.Estado == "SIN SEGUIMIENTO"
                                         select row).Count();
                ViewBag.TicketUsuarioNS = TicketUsuarioNSeg;

                int TicketResueltos = (from row in dbs.Tickets
                                       where row.IDUsuario == user.Id && row.Estado == "RESUELTO"
                                       select row).Count();
                ViewBag.TicketResuelto = TicketResueltos;

                return View();
            }
            else
            {
                return View();
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}