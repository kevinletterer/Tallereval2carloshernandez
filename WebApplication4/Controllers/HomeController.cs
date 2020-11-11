﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {

       

        public ActionResult Calendario(string nombre, string clave)

        {
            return PartialView();
        }

        
        public ActionResult Reservado(string rut, string nombre, string prevision, string centromedico, string especialidad, string medico, string fecha, string hora)
        {   
            ViewBag.nombre = "Se ha agendado correctamente a nombre de "+nombre+", RUT "+rut+", para el dia "+fecha+" a las "+hora+" en "+centromedico+" con el doctor "+medico+".";
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            return PartialView();
        }

        
        
    }
}