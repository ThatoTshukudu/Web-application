using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Models
{
    public class register
    {
        public string email { get; set; }
        public string password { get; set; }

        public Boolean registerState = false;

        //method to register the user
        public void registerUser(string e, string p)
        {
            //try to register the user
            try
            {
                //creating and opening the connection
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();

                //try and store to the database
                string cmd = "insert into pusers values('" + e + "', '" + p + "');";
                SqlCommand command = new SqlCommand(cmd, connect);
                command.ExecuteNonQuery();
                connect.Close();
                //report that registration is successful
                registerState = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to register\n" + ex.Message);
                registerState = false;
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
