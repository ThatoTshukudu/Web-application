using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Models
{
    public class MonetaryDonations
    {
        public string userName { get; set; }
        public string date { get; set; }
        public string amount { get; set; }

        //method to store to databse
        public void storeToDatabase(string UN, string D, string A)
        {
            //try to connect to database
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30");

                connect.Open();

                //query the database
                SqlCommand command = new SqlCommand("insert into monetary values('"+UN+"', '"+D+"', "+ Convert.ToInt16(A) +");", connect);

                command.ExecuteNonQuery();

                connect.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Failed to connect to database \n" + ex.Message);
            }

        }
    }

   

}
