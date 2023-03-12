using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Models
{
    public class GoodsDonations
    {
        public string userName { get; set; }
        public string date  { get; set; }
        public int numberOfItems { get; set; }
        public string category { get; set; }
        public string description { get; set; }

        public void storeToDatabse(string UN, string D, int I, string C, string DESC)
        {
            //try to store on the database
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30");

                //open
                connect.Open();

                //query db
                SqlCommand command = new SqlCommand("insert into goods values('"+UN+"', '"+D+"', "+I+", '"+C+"', '"+DESC+"');", connect);

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
