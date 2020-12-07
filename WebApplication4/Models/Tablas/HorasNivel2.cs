using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication4.Models.Conexion;

namespace WebApplication4.Models.Tablas
{
    public class HorasNivel2
    {
        public string mostrartabla(string Id)
        {

            string conex = Conexion.Conexion.conex;
            var connection = new SqlConnection(conex);
            connection.Open();
            string query = "Select * from HORAS WHERE ID_USUARIO = '" + Id + "'";


            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            string Tabla = "";
            Tabla += "<table class = 'paleBlueRows' border = 2 style = 'width= 60vw'>" +
                        "<th>ID HORA</th>" +
                        "<th>ID USUARIO</th>" +
                        "<th>HORA</th>" +
                        "<th>DISPONIBLE</th>";

            while (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Tabla += "<tr>";

                    Tabla += "<td>" + dataReader.GetInt32(0) + "</td>";
                    Tabla += "<td>" + dataReader.GetInt32(1) + "</td>";
                    Tabla += "<td>" + dataReader.GetDateTime(2).ToString() + "</td>";
                    if (dataReader.GetByte(3)==1)
                        Tabla += "<td>Si</td>";
                    else
                        Tabla += "<td>No</td>";

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