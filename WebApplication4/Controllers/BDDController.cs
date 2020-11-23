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
        // GET: BDD
        public ActionResult Respuestas(string user, string pass)
        {
            
            Token Token = new Token();
            
            if (Token.checkUser(user, pass))
            {
               string token = Token.createToken(user, pass);

               ViewBag.Token = token;
               ViewBag.Mensaje = user+pass+token;

               return View();
            }

            return View("/Views/Home/Login.cshtml");
        }

        public ActionResult ingresarUsuario(string Usuario, string Clave, string Rut, string Telefono, string Oficina, string Especialidad, int Tipo)
        {

            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
            // command.Parameters.AddWithValue("@clave", pass);
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



            return View("/Views/Home/Login.cshtml");
        }

        public ActionResult eliminarUsuario(string Rut)
        {


            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarUsuario";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Rut", Rut);

            // command.Parameters.AddWithValue("@clave", pass);
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


            return View("/Views/Home/Login.cshtml");
        }

        public ActionResult editarUsuario(string Usuario, string Clave, string Rut, string Telefono, string Oficina, string Especialidad, int Tipo)
        {

            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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

            // command.Parameters.AddWithValue("@clave", pass);
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



            return View("/Views/Home/Login.cshtml");
        }

        public ActionResult ingresarPaciente(string Rut, string Nombre, string Telefono, string Email, string Sexo, string Direccion, string Prevision)
        {


            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
            // command.Parameters.AddWithValue("@clave", pass);
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




            return View("/Views/Home/Login.cshtml");
        }


        public ActionResult eliminarPaciente(string Rut)
        {


            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarPaciente";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Rut", Rut);

            // command.Parameters.AddWithValue("@clave", pass);
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


            return View("/Views/Home/Login.cshtml");
        }


        public ActionResult editarPaciente(string Rut, string Nombre, string Telefono, string Email, string Sexo, string Direccion, string Prevision)
        {

            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
            // command.Parameters.AddWithValue("@clave", pass);
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



            return View("/Views/Home/Login.cshtml");
        }

        public ActionResult ingresarServicio(string Servicio, int Precio)
        {


            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarServicio";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", Servicio);
            command.Parameters.AddWithValue("@Precio", Precio);

            // command.Parameters.AddWithValue("@clave", pass);
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




            return View("/Views/Home/Login.cshtml");
        }

        public ActionResult eliminarServicio(string Servicio)
        {


            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarServicio";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Nombre", Servicio);

            // command.Parameters.AddWithValue("@clave", pass);
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


            return View("/Views/Home/Login.cshtml");
        }


        public ActionResult editarServicio(string Servicio, int Precio)
        {

            string conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBBCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarServicio";
            string query2 = "select * from servicios where NOMBRE_SERVICIO ='" + Servicio + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", Servicio);
            command.Parameters.AddWithValue("@Precio", Precio);

            // command.Parameters.AddWithValue("@clave", pass);
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



            return View("/Views/Home/Login.cshtml");
        }
    }
}