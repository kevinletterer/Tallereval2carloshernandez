﻿using System;
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
    public class BDDController : Controller
    {
        
        static Token Token = new Token();
        // GET: BDD
        public static string conex = Conexion.conex;
        static Usuarios usuarios = new Usuarios();
        static Pacientes pacientes = new Pacientes();
        static Horas horas = new Horas();
        static Servicios servicios = new Servicios();
        static Citas citas = new Citas();


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
                    
                    ViewBag.Table = usuarios.mostrartabla();

                    return View("/Views/Administrador/Usuarios.cshtml");
                }
                if (type == 2)
                {
                    Session["user"] = user;
                    Session["pass"] = pass;
                    int IdUsuario = Token.getUserId(user, pass);

                    HorasNivel2 horas2 = new HorasNivel2();
                    CitasNivel2 citas2 = new CitasNivel2();
                    PacientesNivel2 pacientes2 = new PacientesNivel2();

                    ViewBag.IdUsuario = IdUsuario.ToString();
                    ViewBag.Table1 = horas2.mostrartabla(IdUsuario.ToString());
                    ViewBag.Table2 = citas2.mostrartabla(IdUsuario.ToString());
                    ViewBag.Table3 = pacientes2.mostrartabla(IdUsuario.ToString());

                    return View("/Views/Nivel2/Medico.cshtml");
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



        public ActionResult ingresarUsuario(string Nombre, string Usuario, string Clave, string Rut, string Telefono, string Oficina, string Especialidad, int Tipo)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarUsuario";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", Nombre);
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

                ViewBag.mensaje = "Error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Usuario agregado correctamente ";
            }
            connection.Close();

            ViewBag.Table = usuarios.mostrartabla();

            return View("/Views/Administrador/Usuarios.cshtml");
        }

        public ActionResult eliminarUsuario(string Id)
        {
            string token = Session["Token"].ToString();
                if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarUsuario";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Id", Id);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Usuario eliminado correctamente ";
            }
            connection.Close();

            ViewBag.Table = usuarios.mostrartabla();

            return View("/Views/Administrador/Usuarios.cshtml");
        }

        public ActionResult editarUsuario(string Id, string Nombre, string Usuario, string Clave, string Rut, string Telefono, string Oficina, string Especialidad, int Tipo)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarUsuario";
            string query2 = "select * from usuarios where ID_USUARIO ='" + Id + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Nombre", Nombre);
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
                    ViewBag.mensaje = " No existe el usuario con ID " + Id + " en los registros.";
                }
                else
                {
                    ViewBag.mensaje = "Usuario editado correctamente";
                    command.ExecuteNonQuery();
                }

            }
            catch
            {

            }


            connection.Close();

            ViewBag.Table = usuarios.mostrartabla();

            return View("/Views/Administrador/Usuarios.cshtml");
        }

        public ActionResult ingresarPaciente(string Rut, string Nombre, string Telefono, string Email, string Sexo, string Direccion, string Prevision)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

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

                ViewBag.mensaje = "Error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Paciente agregado correctamente ";
            }
            connection.Close();


            ViewBag.Table = pacientes.mostrartabla();

            return View("/Views/Administrador/Pacientes.cshtml");
        }


        public ActionResult eliminarPaciente(string Id)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarPaciente";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Id", Id);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Paciente eliminado correctamente ";
            }
            connection.Close();

            ViewBag.Table = pacientes.mostrartabla();
            return View("/Views/Administrador/Pacientes.cshtml");
        }


        public ActionResult editarPaciente(string Id, string Rut, string Nombre, string Telefono, string Email, string Sexo, string Direccion, string Prevision)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarPaciente";
            string query2 = "select * from pacientes where ID_PACIENTE ='" + Id + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", Id);
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
                ViewBag.mensaje = " No existe el paciente con ID " + Id + " en los registros.";
            }
            else
            {
                ViewBag.mensaje = "Paciente editado correctamente";
                command.ExecuteNonQuery();
            }



            connection.Close();


            ViewBag.Table = pacientes.mostrartabla();
            return View("/Views/Administrador/Pacientes.cshtml");
        }

        public ActionResult ingresarServicio(string Nombre, int Precio, string Especialidad)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarServicio";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", Nombre);
            command.Parameters.AddWithValue("@Precio", Precio);
            command.Parameters.AddWithValue("@Especialidad", Especialidad);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Servicio agregado correctamente ";
            }
            connection.Close();



            ViewBag.Table = servicios.mostrartabla();
            return View("/Views/Administrador/Servicios.cshtml");
        }

        public ActionResult eliminarServicio(int Id)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarServicio";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Id", Id);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Servicio eliminado correctamente ";
            }
            connection.Close();

            ViewBag.Table = servicios.mostrartabla();
            return View("/Views/Administrador/Servicios.cshtml");
        }


        public ActionResult editarServicio(int Id, string Nombre, int Precio, string Especialidad)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarServicio";
            string query2 = "select * from servicios where ID_SERVICIO ='" + Id + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Nombre", Nombre);
            command.Parameters.AddWithValue("@Precio", Precio);
            command.Parameters.AddWithValue("@Especialidad", Especialidad);

            ViewBag.mensaje = "";

            int row_count = command2.ExecuteNonQuery();
            if (row_count == 0)
            {
                ViewBag.mensaje = "No existe el servicio con ID " + Id + " en los registros.";
            }
            else
            {
                ViewBag.mensaje = "Servicio editado correctamente";
                command.ExecuteNonQuery();
            }



            connection.Close();


            ViewBag.Table = servicios.mostrartabla();
            return View("/Views/Administrador/Servicios.cshtml");
        }

        public ActionResult ingresarHora(string Id_Usuario, string Fecha, string Hora, string Disponible)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarHora";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            DateTime HoraCompuesta = DateTime.Parse(Fecha + " " + Hora);
            Byte DisponibleByte = Byte.Parse((Disponible == "Si" ? 1 : 0).ToString());

            command.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
            command.Parameters.AddWithValue("@Hora", HoraCompuesta);
            command.Parameters.AddWithValue("@Disponible", DisponibleByte);
            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Hora agregada correctamente ";
            }
            connection.Close();

            ViewBag.Table = horas.mostrartabla();

            return View("/Views/Administrador/Horas.cshtml");
        }

        public ActionResult eliminarHora(string Id)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarHora";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            ;
            command.Parameters.AddWithValue("@Id", Id);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Hora eliminada correctamente ";
            }
            connection.Close();

            ViewBag.Table = horas.mostrartabla();
            return View("/Views/Administrador/Horas.cshtml");
        }

        public ActionResult editarHora(string Id, string Id_Usuario, string Fecha, string Hora, string Disponible)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarHora";
            string query2 = "select * from horas where ID_HORA ='" + Id + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;

            DateTime HoraCompuesta = DateTime.Parse(Fecha + " " + Hora);
            Byte DisponibleByte = Byte.Parse((Disponible == "Si" ? 1 : 0).ToString());

            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
            command.Parameters.AddWithValue("@Hora", HoraCompuesta);
            command.Parameters.AddWithValue("@Disponible", DisponibleByte);

            ViewBag.mensaje = "";

            int row_count = command2.ExecuteNonQuery();
            if (row_count == 0)
            {
                ViewBag.mensaje = " No existe la hora ID " + Id + " en los registros.";
            }
            else
            {
                ViewBag.mensaje = "Hora editada correctamente";
                command.ExecuteNonQuery();
            }



            connection.Close();


            ViewBag.Table = horas.mostrartabla();
            return View("/Views/Administrador/Horas.cshtml");
        }

        public ActionResult ingresarCita(string Id_Paciente, string Id_Usuario, string Id_Servicio, string Id_Hora)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarCita";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;


            command.Parameters.AddWithValue("@Id_Paciente", Id_Paciente);
            command.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
            command.Parameters.AddWithValue("@Id_Servicio", Id_Servicio);
            command.Parameters.AddWithValue("@Id_Hora", Id_Hora);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Cita agregada correctamente ";
            }
            connection.Close();

            ViewBag.Table = citas.mostrartabla();

            return View("/Views/Administrador/Citas.cshtml");
        }

        public ActionResult eliminarCita(string Id)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarCita";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            
            command.Parameters.AddWithValue("@Id", Id);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Cita eliminada correctamente ";
            }
            connection.Close();

            ViewBag.Table = citas.mostrartabla();
            return View("/Views/Administrador/Citas.cshtml");
        }

        public ActionResult editarCita(string Id, string Id_Paciente, string Id_Usuario, string Id_Servicio, string Id_Hora)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarCita";
            string query2 = "select * from citas where ID_CITA ='" + Id + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;


            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Id_Paciente", Id_Paciente);
            command.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
            command.Parameters.AddWithValue("@Id_Servicio", Id_Servicio);
            command.Parameters.AddWithValue("@Id_Hora", Id_Hora);

            ViewBag.mensaje = "";

            int row_count = command2.ExecuteNonQuery();
            if (row_count == 0)
            {
                ViewBag.mensaje = " No existe la cita ID " + Id + " en los registros.";
            }
            else
            {
                ViewBag.mensaje = "Cita ID " +Id+ " editado correctamente";
                command.ExecuteNonQuery();
            }



            connection.Close();


            ViewBag.Table = citas.mostrartabla();
            return View("/Views/Administrador/Citas.cshtml");
        }

        public ActionResult ingresarHora2(string Id_Usuario, string Fecha, string Hora, string Disponible)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }



            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "guardarHora";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            DateTime HoraCompuesta = DateTime.Parse(Fecha + " " + Hora);
            Byte DisponibleByte = Byte.Parse((Disponible == "Si" ? 1 : 0).ToString());

            command.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
            command.Parameters.AddWithValue("@Hora", HoraCompuesta);
            command.Parameters.AddWithValue("@Disponible", DisponibleByte);
            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al agregar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Hora agregada correctamente ";
            }
            connection.Close();

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

            return View("/Views/Nivel2/Medico.cshtml");
        }

        public ActionResult eliminarHora2(string Id_Usuario, string Id)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "eliminarHora2";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);

            ViewBag.mensaje = "";


            int row_count = command.ExecuteNonQuery();

            if (row_count == 0)
            {

                ViewBag.mensaje = "Error al eliminar";

            }
            else if (row_count > 0)
            {
                ViewBag.mensaje = "Hora eliminada correctamente ";
            }
            connection.Close();

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
            return View("/Views/Nivel2/Medico.cshtml");
        }

        public ActionResult editarHora2(string Id, string Id_Usuario, string Fecha, string Hora, string Disponible)
        {
            string token = Session["Token"].ToString();
            if (!Token.checkTokenValid(token))
            {
                Session["Token"] = "";
                ViewBag.mensaje = "Tiempo de sesión expirado";
                return PartialView("/Views/Home/Login.cshtml");
            }

            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "editarHora2";
            string query2 = "select * from horas where ID_HORA ='" + Id + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command2 = new SqlCommand(query2, connection);

            command.CommandType = System.Data.CommandType.StoredProcedure;

            DateTime HoraCompuesta = DateTime.Parse(Fecha + " " + Hora);
            Byte DisponibleByte = Byte.Parse((Disponible == "Si" ? 1 : 0).ToString());

            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
            command.Parameters.AddWithValue("@Hora", HoraCompuesta);
            command.Parameters.AddWithValue("@Disponible", DisponibleByte);

            ViewBag.mensaje = "";

            int row_count = command2.ExecuteNonQuery();
            if (row_count == 0)
            {
                ViewBag.mensaje = " No existe la hora ID " + Id + " en los registros.";
            }
            else
            {
                ViewBag.mensaje = "Hora editada correctamente";
                command.ExecuteNonQuery();
            }



            connection.Close();


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
            return View("/Views/Nivel2/Medico.cshtml");
        }
    }
}