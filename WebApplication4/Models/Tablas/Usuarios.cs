using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication4.Models.Conexion;

namespace WebApplication4.Models.Tablas
{
    public class Usuarios
    {
        public string mostrartabla()
        {

            string conex = Conexion.Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "Select * from USUARIOS";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Tabla = "";
            Tabla += "<table class = 'paleBlueRows' border = 2 style = 'width= 60vw'>" +
                        "<th>ID</th>" +
                        "<th>NOMBRE</th>" +
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
                    Tabla += "<td>" + dataReader.GetString(8) + "</td>";
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

            connection.Close();


            return Tabla;
        }
    }
}