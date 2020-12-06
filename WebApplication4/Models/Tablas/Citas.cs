using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication4.Models.Conexion;

namespace WebApplication4.Models.Tablas
{
    public class Citas
    {
        public string mostrartabla()
        {

            string conex = Conexion.Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "Select * from CITAS";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Tabla = "";
            Tabla += "<table border = 2 style = 'width= 60vw'>" +
                        "<th>ID CITA</th>" +
                        "<th>ID  Paciente</th>" +
                        "<th>ID USUARIO</th>" +
                        "<th>ID SERVICIO</th>" +
                        "<th>ID HORA</th>" ;

            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Tabla += "<tr>";

                    Tabla += "<td>" + dataReader.GetInt32(0) + "</td>";
                    Tabla += "<td>" + dataReader.GetInt32(1) + "</td>";
                    Tabla += "<td>" + dataReader.GetInt32(2) + "</td>";
                    Tabla += "<td>" + dataReader.GetInt32(3) + "</td>";
                    Tabla += "<td>" + dataReader.GetInt32(4) + "</td>";
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