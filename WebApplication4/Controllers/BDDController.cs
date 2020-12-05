using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication4.Models.Token;

namespace WebApplication4.Controllers
{
    public class BDDController : Controller
    {
        static Token Token = new Token();
        // GET: BDD
        public static string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BDDCRONOS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";





        public ActionResult Respuestas(string user, string pass)
        {
            ViewBag.mensaje = "";

            if (Token.checkUser(user, pass))
            {
               string token = Token.createToken(user, pass);
                Session["Token"] = token;

                int type = Token.getUserType(user, pass);

              
                if (type == 1)
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
                                "<th>NIVEL</th>";

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
                            Tabla += "<td>" + dataReader.GetInt32(7) + "</td>";
                            Tabla += "</tr>";

                        }

                        dataReader.NextResult();
                    }
                    Tabla += "</table>";

                    ViewBag.Table = Tabla;
                    connection.Close();

                    return View("/Views/Administrador/Usuarios.cshtml");
                }
                if (type == 2)
                {
                    return View("/Views/BDD/RespuestaPersonal.cshtml");
                }


            }
            else
            {

                    ViewBag.mensaje = "Nombre de usuario y/o clave incorrectos.";             
            }

            return View("/Views/Home/Login.cshtml");
        }

        public class HomeController : Controller
        {
            [HttpGet]
            public ActionResult Index()
            {
                return View();
            }

      
        }



        public ActionResult ingresarUsuario(string Usuario, string Clave, string Rut, string Telefono, string Oficina, string Especialidad, int Tipo)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                return PartialView("/Views/Home/Index.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarUsuario";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Usuario", Usuario);
            command.Parameters.AddWithValue("@Clave", Clave);
            command.Parameters.AddWithValue("@Rut", Rut);
            command.Parameters.AddWithValue("@Telefono", Telefono);
            command.Parameters.AddWithValue("@Oficina", Oficina);
            command.Parameters.AddWithValue("@Especialidad", Especialidad);
            command.Parameters.AddWithValue("@Tipo", Tipo);
            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "usuario agregado correctamente ";
            }
            connection.Close();



            return View("/Views/Administrador/Usuarios.cshtml");
        }

        public ActionResult eliminarUsuario(string Rut)
        {
            string token = Session["Token"].ToString();
                if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                return PartialView("/Views/Home/Index.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarUsuario";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Rut", Rut);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "usuario eliminado correctamente ";
            }
            connection.Close();


            return View("/Views/Administrador/Usuarios.cshtml");
        }

        public ActionResult editarUsuario(string Usuario, string Clave, string Rut, string Telefono, string Oficina, string Especialidad, int Tipo)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                return PartialView("/Views/Home/Index.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarUsuario";
            string query2 = "select * from usuarios where usuario ='" + Usuario + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Usuario", Usuario);
            command.Parameters.AddWithValue("@Clave", Clave);
            command.Parameters.AddWithValue("@Rut", Rut);
            command.Parameters.AddWithValue("@Telefono", Telefono);
            command.Parameters.AddWithValue("@Oficina", Oficina);
            command.Parameters.AddWithValue("@Especialidad", Especialidad);
            command.Parameters.AddWithValue("@Tipo", Tipo);

            ViewBag.mensaje = "";

            try
            {

                int row_count = command2.ExecuteNonQuery();
                if (row_count == 0)
                {
                    ViewBag.mensaje = " no existe el usuario " + Usuario + " en los registros.";
                }
                else
                {
                    command.ExecuteNonQuery();
                }

            }
            catch
            {

            }


            connection.Close();



            return View("/Views/Administrador/Usuarios.cshtml");
        }

        public ActionResult ingresarPaciente(string Rut, string Nombre, string Telefono, string Email, string Sexo, string Direccion, string Prevision)
        {


            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarPaciente";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Rut", Rut);
            command.Parameters.AddWithValue("@Nombre", Nombre);
            command.Parameters.AddWithValue("@Telefono", Telefono);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Sexo", Sexo);
            command.Parameters.AddWithValue("@Direccion", Direccion);
            command.Parameters.AddWithValue("@Prevision", Prevision);
            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "paciente agregado correctamente ";
            }
            connection.Close();




            return View("/Views/Administrador/Pacientes.cshtml");
        }


        public ActionResult eliminarPaciente(string Rut)
        {


            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarPaciente";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Rut", Rut);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "usuario eliminado correctamente ";
            }
            connection.Close();


            return View("/Views/Administrador/Pacientes.cshtml");
        }


        public ActionResult editarPaciente(string Rut, string Nombre, string Telefono, string Email, string Sexo, string Direccion, string Prevision)
        {

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarPaciente";
            string query2 = "select * from usuarios where usuario ='" + Rut + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Rut", Rut);
            command.Parameters.AddWithValue("@Nombre", Nombre);
            command.Parameters.AddWithValue("@Telefono", Telefono);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Sexo", Sexo);
            command.Parameters.AddWithValue("@Direccion", Direccion);
            command.Parameters.AddWithValue("@Prevision", Prevision);
            ViewBag.mensaje = "";

            int row_count = command2.ExecuteNonQuery();
            if (row_count == 0)
            {
                ViewBag.mensaje = " no existe el paciente " + Rut + " en los registros.";
            }
            else
            {
                command.ExecuteNonQuery();
            }



            connection.Close();



            return View("/Views/Administrador/Pacientes.cshtml");
        }

        public ActionResult ingresarServicio(string Servicio, int Precio)
        {


            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarServicio";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", Servicio);
            command.Parameters.AddWithValue("@Precio", Precio);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "servicio agregado correctamente ";
            }
            connection.Close();




            return View("/Views/Administrador/Servivios.cshtml");
        }

        public ActionResult eliminarServicio(string Servicio)
        {


            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarServicio";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Nombre", Servicio);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "usuario eliminado correctamente ";
            }
            connection.Close();


            return View("/Views/Administrador/Servivios.cshtml");
        }


        public ActionResult editarServicio(string Servicio, int Precio)
        {

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarServicio";
            string query2 = "select * from servicios where NOMBRE_SERVICIO ='" + Servicio + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", Servicio);
            command.Parameters.AddWithValue("@Precio", Precio);

            ViewBag.mensaje = "";

            int row_count = command2.ExecuteNonQuery();
            if (row_count == 0)
            {
                ViewBag.mensaje = " no existe el servicio " + Servicio + " en los registros.";
            }
            else
            {
                command.ExecuteNonQuery();
            }



            connection.Close();



            return View("/Views/Administrador/Servivios.cshtml");
        }
    }
}