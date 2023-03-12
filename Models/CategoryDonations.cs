using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Models
{
    public class CategoryDonations
    {
        public string category { get; set; }

        //store to db
        public void storeToDatabase(string data)
        {
            //connect and store
            try
            {
                //object
                SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30");

                //open
                connect.Open();

                //query
                SqlCommand command = new SqlCommand("insert into category(category) values ('"+data+"')", connect);

                command.ExecuteNonQuery();

                connect.Close();

                Console.WriteLine("Stored");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);                
            }
        }
    }
}
