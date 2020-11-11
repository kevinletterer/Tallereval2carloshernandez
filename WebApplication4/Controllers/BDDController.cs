using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace WebApplication4.Controllers
{
    public class BDDController : Controller
    {
        // GET: BDD
        public ActionResult Respuestas(string user, string pass)
        {


            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDDCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "select * from USUARIOS where USUARIO = '"+user+"' AND CLAVE = '"+pass+"'";
            SqlCommand command;
            SqlDataReader dataReader;
            string result = "";

            command = new SqlCommand(query,connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
                {
                result = dataReader.GetString(0);
                }

            var mensaje = "";
            if (result==user)
            {
                mensaje = "existe";

            }
            else 
            {
                mensaje = "no existe";
            }
            ViewBag.mensaje = mensaje;

            connection.Close();

            return View();
        }
    }
}