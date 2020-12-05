using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication4.Models.Token;

namespace WebApplication4.Controllers
{
    public class AdministradorController : Controller
    {
        static Token Token = new Token();
        public static string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BDDCRONOS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // GET: Administrador
        public ActionResult Usuarios()
        {

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "Select * from USUARIOS";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Tabla = "";
            Tabla += "<table border = 2 style = 'width= 60vw'>" +
                        "<th>ID</th>" +
                        "<th>USUARIO</th>" +
                        "<th>CLAVE</th>" +
                        "<th>RUT</th>" +
                        "<th>TELEFONO</th>" +
                        "<th>OFICINA</th>" +
                        "<th>ESPECIALIDAD</th>" +
                        "<th>NIVEL</th>" ;

            while (dataReader.HasRows)
            { 
                while (dataReader.Read())
                {
                    Tabla += "<tr>";
                    Tabla += "<td>" + dataReader.GetInt32(0) + "</td>";
                    Tabla += "<td>" + dataReader.GetString(1) + "</td>";
                    Tabla += "<td>" + dataReader.GetString(2) + "</td>";
                    Tabla += "<td>" + dataReader.GetString(3) + "</td>";
                    Tabla += "<td>" + dataReader.GetString(4) + "</td>";
                    Tabla += "<td>" + dataReader.GetString(5) + "</td>";
                    Tabla += "<td>" + dataReader.GetString(6) + "</td>";
                    Tabla += "<td>" + dataReader.GetInt32(7)  + "</td>";
                    Tabla += "</tr>";

                }
                
                dataReader.NextResult();
            }
            Tabla += "</table>";

            ViewBag.Table = Tabla;
            connection.Close();

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
        
    }

}