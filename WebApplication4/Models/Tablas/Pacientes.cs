using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models.Conexion;
using System.Data.SqlClient;

namespace WebApplication4.Models.Tablas
{
    public class Pacientes
    {
        public string mostrartabla()
        {

            string conex = Conexion.Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "Select * from PACIENTES";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Tabla = "";
            Tabla += "<table class = 'paleBlueRows' border = 2 style = 'width= 60vw'>" +
                        "<th>ID</th>" +
                        "<th>RUT</th>" +
                        "<th>NOMBRE</th>" +
                        "<th>TELEFONO</th>" +
                        "<th>EMAIL</th>" +
                        "<th>SEXO</th>" +
                        "<th>DIRECCION</th>" +
                        "<th>PREVISION</th>";

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
                    Tabla += "<td>" + dataReader.GetString(7) + "</td>";
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