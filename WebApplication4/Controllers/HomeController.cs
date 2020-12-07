using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models.Conexion;
using System.Data.SqlClient;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Calendario(string nombre, string clave)

        {
            return PartialView();
        }

        
        public ActionResult Reservado(string Hora)
        {
            Session["Hora"] = Hora;

            string nombre = Session["Nombre"].ToString();
            string rut = Session["Rut"].ToString();
            string fecha = Session["Fecha"].ToString();
            string hora = Session["Hora"].ToString();
            string email = Session["Email"].ToString();
            string sexo = Session["Sexo"].ToString();
            string prevision = Session["Prevision"].ToString();
            string direccion = Session["Direccion"].ToString();
            string telefono = Session["Telefono"].ToString();
            string usuario = Session["Usuario"].ToString();
            string especialidad = Session["Especialidad"].ToString();
            string conex = Conexion.conex;
            string IdUsuario ="";
            string servicio = Session["Servicio"].ToString(); 

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "select ID_USUARIO from USUARIOS where NOMBRE ='" + usuario + "'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            while (dataReader.HasRows){
                while (dataReader.Read()){
                    IdUsuario = dataReader.GetInt32(0).ToString(); ;
                }
                dataReader.NextResult();
            }
            connection.Close();
            connection.Open();

            string query2 = "guardarPaciente";
            command = new SqlCommand(query2, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Rut", rut);
            command.Parameters.AddWithValue("@Nombre", nombre);
            command.Parameters.AddWithValue("@Telefono", telefono);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Sexo", sexo);
            command.Parameters.AddWithValue("@Direccion", direccion);
            command.Parameters.AddWithValue("@Prevision", prevision);
            command.ExecuteNonQuery();

            connection.Close();
            connection.Open();

            string query3 = "select ID_PACIENTE from PACIENTES where RUT = '"+rut+"' AND NOMBRE = '"+nombre+"'";
            command = new SqlCommand(query3, connection);
            dataReader = command.ExecuteReader();
            string IdPaciente="";
            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    IdPaciente = dataReader.GetInt32(0).ToString();
                }
                dataReader.NextResult();
            }

            connection.Close();
            connection.Open();

            string query4 = "select ID_HORA from HORAS where HORA = '" +DateTime.Parse(fecha+" "+hora).ToString()+ "' AND ID_USUARIO = '" + IdUsuario + "'";
            command = new SqlCommand(query4, connection);
            dataReader = command.ExecuteReader();
            string IdHora = "";
            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    IdHora = dataReader.GetInt32(0).ToString();
                }
                dataReader.NextResult();
            }

            connection.Close();
            connection.Open();

            string query5 = "select ID_Servicio from SERVICIOS where NOMBRE_SERVICIO = '" + servicio + "' AND ESPECIALIDAD = '" + especialidad + "'";
            command = new SqlCommand(query5, connection);
            dataReader = command.ExecuteReader();
            string IdServicio = "";
            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    IdServicio = dataReader.GetInt32(0).ToString();
                }
                dataReader.NextResult();
            }

            connection.Close();
            connection.Open();

            string query6 = "guardarCita";
            command = new SqlCommand(query6, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;


            command.Parameters.AddWithValue("@Id_Paciente", IdPaciente);
            command.Parameters.AddWithValue("@Id_Usuario", IdUsuario);
            command.Parameters.AddWithValue("@Id_Servicio", IdServicio);
            command.Parameters.AddWithValue("@Id_Hora", IdHora);

            ViewBag.mensaje = "";


            command.ExecuteNonQuery();

            connection.Close();
            connection.Open();


            string query7 = "apartarHora";
            command = new SqlCommand(query7, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", IdHora);
            command.ExecuteNonQuery();
            

            connection.Close();




            ViewBag.nombre = "Se ha agendado correctamente a nombre de "+nombre+", RUT "+rut+", para el dia "+fecha+" a las "+hora+" con el doctor "+usuario+".";
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
        public ActionResult Main2(string Rut, string Nombre, string Telefono, string Email, string Sexo, string Direccion, string Prevision)
        {

            Session["Rut"] = Rut;
            Session["Nombre"] = Nombre;
            Session["Telefono"] = Telefono;
            Session["Email"] = Email;
            Session["Sexo"] = Sexo;
            Session["Prevision"] = Prevision;
            Session["Direccion"] = Direccion;

            string conex = Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "select distinct Especialidad from USUARIOS where TIPO = 2";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Options = "";
            

            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {

                    Options += "<option>" + dataReader.GetString(0) + "</option>";

                }

                dataReader.NextResult();
            }

            connection.Close();

            ViewBag.options = Options; 
            return View();
        }
        public ActionResult Main3(string especialidad)
        {
            Session["Especialidad"] = especialidad;

            string conex = Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "select Nombre from USUARIOS where Especialidad ='"+especialidad+"'";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Options = "";


            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {

                    Options += "<option>" + dataReader.GetString(0) + "</option>";

                }

                dataReader.NextResult();
            }

            connection.Close();

            ViewBag.options = Options;
            return View();

        }

        public ActionResult Main4(string Usuario)
        {
            Session["Usuario"] = Usuario;
            string especialidad = Session["Especialidad"].ToString();
            string conex = Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "select NOMBRE_SERVICIO from SERVICIOS where Especialidad ='" + especialidad + "'";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Options = "";


            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {

                    Options += "<option>" + dataReader.GetString(0) + "</option>";

                }

                dataReader.NextResult();
            }

            connection.Close();

            ViewBag.options = Options;
            return View();

        }
        public ActionResult Main5(string Servicio)
        {
            Session["Servicio"] = Servicio;
            string Usuario = Session["Usuario"].ToString();
            string conex = Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            
            string query = "select Hora from HORAS where ID_USUARIO =(SELECT ID_USUARIO FROM USUARIOS WHERE NOMBRE ='" + Usuario + "' AND DISPONIBLE = '1')";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Options = "";

            string fecha2 = "";
            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    

                    String fecha = dataReader.GetDateTime(0).ToShortDateString();
                    if (fecha != fecha2)
                    {
                        Options += "<option>" + fecha + "</option>";
                    }
                    

                    fecha2 = fecha;
                }

                dataReader.NextResult();
            }

            connection.Close();

            ViewBag.options = Options;
            return View();

            
        }
        public ActionResult Main6(string Fecha)
        {
            string usuario = Session["Usuario"].ToString();
            Session["Fecha"] = Fecha;
            string conex = Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            
            string query = "select Hora from HORAS where ID_USUARIO =(SELECT ID_USUARIO FROM USUARIOS WHERE NOMBRE ='" + usuario + "' AND DISPONIBLE = '1')";

            

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Options = "";

            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {


                    String hora = dataReader.GetDateTime(0).ToShortTimeString();
                    
                    Options += "<option>" + hora + "</option>";
                    

                }

                dataReader.NextResult();
            }

            connection.Close();

            ViewBag.options = Options;
            return View();
        }

        public ActionResult Login()
        {
            Session["Token"] = "";
            return PartialView();
        }
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Examenes()
        {
            return PartialView("/Views/Especialidades/Examenes.cshtml");
        }

        public ActionResult Imagenologia()
        {
            return PartialView("/Views/Especialidades/Imagenologia.cshtml");
        }

        public ActionResult Dental()
        {
            return PartialView("/Views/Especialidades/Dental.cshtml");
        }

        public ActionResult Logout()
        {
            Session["Token"] = "";
            return PartialView("/Views/Home/Index.cshtml");
        }
    }
}