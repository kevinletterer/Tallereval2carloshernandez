using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models.Token;
using WebApplication4.Models.Tablas;

namespace WebApplication4.Controllers
{
    public class Nivel2Controller : Controller
    {
        // GET: Nivel2
        static Token Token = new Token();
        public ActionResult Medico()
        {

            string user = Session["user"].ToString();
            string pass = Session["pass"].ToString();

            int IdUsuario = Token.getUserId(user, pass);

            HorasNivel2 horas2 = new HorasNivel2();
            CitasNivel2 citas2 = new CitasNivel2();
            PacientesNivel2 pacientes2 = new PacientesNivel2();

            ViewBag.IdUsuario = IdUsuario.ToString();
            ViewBag.Table1 = horas2.mostrartabla(IdUsuario.ToString());
            ViewBag.Table2 = citas2.mostrartabla(IdUsuario.ToString());
            ViewBag.Table3 = pacientes2.mostrartabla(IdUsuario.ToString());

            return View();
        }
    }
}