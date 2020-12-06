using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication4.Models.Token;
using WebApplication4.Models.Conexion;
using WebApplication4.Models.Tablas;


namespace WebApplication4.Controllers
{
    public class AdministradorController : Controller
    {
        static string conex = Conexion.conex;
        static Token Token = new Token();
        // GET: Administrador
        public ActionResult Usuarios()
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            Usuarios usuarios = new Usuarios();
            ViewBag.Table = usuarios.mostrartabla();
           
            return View("/Views/Administrador/Usuarios.cshtml");
        }
        public ActionResult Citas()
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            Citas citas = new Citas();
            ViewBag.Table = citas.mostrartabla();
            return View("/Views/Administrador/Citas.cshtml");
        }
        public ActionResult Horas()
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            Horas horas = new Horas();
            ViewBag.Table = horas.mostrartabla();
            return View("/Views/Administrador/Horas.cshtml");
        }
        public ActionResult Pacientes()
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            Pacientes pacientes = new Pacientes();
            ViewBag.Table = pacientes.mostrartabla();
            return View("/Views/Administrador/Pacientes.cshtml");
        }
        public ActionResult Servicios()


        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            Servicios servicios = new Servicios();
            ViewBag.Table = servicios.mostrartabla();
            return View("/Views/Administrador/Servicios.cshtml");
        }
        
    }

}