using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace DisasterAlleviationFoundation.Models
{
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }

        public string validateDetails(string e, string p)
        {
            //try to connect to the databse
            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();

                string query = "select * from pusers where email = '" + e + "' and password = '" + p + "';";
                SqlCommand cmd = new SqlCommand(query, connect);
                //cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    connect.Close();
                    return "admin";
                }
                else
                {

                    reader.Close();

                    //check if the user is a normal user
                    string normalQuery = "select * from pusers where email = '" + e + "' and password = '" + p + "';";
                    SqlCommand normalCmd = new SqlCommand(normalQuery, connect);

                    SqlDataReader normalReader = normalCmd.ExecuteReader();

                    if (normalReader.Read())
                    {
                        connect.Close();
                        return "normal";
                    }
                    else
                    {
                        return "none";
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to complete tasks to database\n\n" + ex.Message);
                return  "none";
            }
        }

        //method for hashing
        public string hashPass(string pass)
        {
            StringBuilder sb = new StringBuilder();

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashByte = md5.ComputeHash(Encoding.UTF8.GetBytes(pass));

                foreach (byte b in hashByte)
                {
                    sb.Append(b.ToString("X2"));
                }
            }

            return sb.ToString();

        }

    }
}
