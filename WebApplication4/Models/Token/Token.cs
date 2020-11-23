using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApplication4.Models.Token
{
    public class Token

    {
        private static String conex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDDCRONOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection connection = new SqlConnection(conex);
        private SqlCommand command;
        private SqlDataReader dataReader;
        private static Random random = new Random();

        public int checkToken(String token)
        {
            if (!checkTokenNull(token))
                return 0;

            if (!checkTokenExists(token))
                return 1;

            if (!checkTokenValid(token))
                return 2;

            return 3;
        }


        public string createToken(string user, string pass)
        {
            string token = random.Next(100000000, 999999999).ToString();
            string query = "insert into TOKENS (TOKEN, ID_USUARIO) values ('" + token + "','" + getUserId(user, pass).ToString() + "')";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return token;
        }

        public int getUserId(string user, string pass)
        {
            int userid = 0;
            connection.Open();
            string query = "select ID_USUARIO from USUARIOS where USUARIO = '" + user + "' AND CLAVE = '" + pass + "'";
            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                userid = dataReader.GetInt32(0);
            }
            connection.Close();

            return userid;
        }

        public int getUserType(string user, string pass)
        {
            int usertype = 0;
            connection.Open();
            string query = "select TIPO from USUARIOS where USUARIO = '" + user + "' AND CLAVE = '" + pass + "'";
            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                usertype = dataReader.GetInt32(0);
            }
            connection.Close();

            return usertype;
        }
        public bool checkUser(String user, String pass)
        {
            connection.Open();
            string query = "select Count(*) from USUARIOS where USUARIO = '" + user + "' AND CLAVE = '" + pass + "'";

            int result = 0;

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                result = dataReader.GetInt32(0);
            }
            connection.Close();

            return result == 1 ? true : false;

        }

        private bool checkTokenNull(String token)

        {
            if (String.IsNullOrEmpty(token))
            {
                return false;
            }
            return true;
        }

        private bool checkTokenExists(String token)
        {
            connection.Open();
            string query = "select COUNT(*) from TOKENS where TOKEN = '" + token + "'";
            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            string result = "";

            while (dataReader.Read())
            {
                result = dataReader.GetInt32(0).ToString();
            }


            connection.Close();

            return int.Parse(result) > 0 ? true : false;
        }

        public bool checkTokenValid(String token)
        {
            DateTime tokentime = getTokenTime(token);
            DateTime check = DateTime.Now;

            tokentime = tokentime.AddMinutes(5);
            int compare = DateTime.Compare(tokentime, check);

            return compare == 1 ? true : false;
        }

        public DateTime getTokenTime(String token)
        {
            connection.Open();
            string query = "select TIME from TOKENS where TOKEN = '" + token + "'";
            // query = " select InsertarNombre (Carlos, Pinto) "

            DateTime time = DateTime.Now;

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            dataReader.Read();
            time = dataReader.GetDateTime(0);
            connection.Close();

            return time;
        }
    }

}