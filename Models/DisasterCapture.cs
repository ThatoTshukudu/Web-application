using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Models
{
    public class DisasterCapture
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string aid { get; set; }

        public void storeToDatabse(string SD, string ED, string L, string D, string A)
        {
            //try to store on the database
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30");

                //open
                connect.Open();

                //query db
                SqlCommand command = new SqlCommand("insert into disaster(startDate, endDate, location, description, aid) values('" + SD + "', '" + ED + "', '" + L + "', '" + D + "', '" + A + "');", connect);

                command.ExecuteNonQuery();

                connect.Close();

                Console.WriteLine("Stored");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect/store on database\n" + ex.Message);
            }
        }
    }
}
