using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult Usuarios()
        {
            return View("/Views/Administrador/Usuarios.cshtml");
        }
        public ActionResult Citas()
        {
            return View("/Views/Administrador/Citas.cshtml");
        }
        public ActionResult Horas()
        {
            return View("/Views/Administrador/Horas.cshtml");
        }
        public ActionResult Pacientes()
        {
            return View("/Views/Administrador/Pacientes.cshtml");
        }
        public ActionResult Servicios()
        {
            return View("/Views/Administrador/Servicios.cshtml");
        }
        public ActionResult Logout()
        {
            ViewBag.Token = "";
            return PartialView("/Views/Home/Index.cshtml");
        }
    }

}