using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class allocateMoneyModel : PageModel
    {
        //List of disasters
        public List<ManageDisaster> myList = new List<ManageDisaster>();

        //monetary amount in bank
        public int total;

        //prop
        [BindProperty]
        public AllocateMoney myAllo { get; set; }

        public void OnGet()
        {
            try
            {
                //get the disaster location and description
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    //open
                    connect.Open();

                    using (SqlCommand command = new SqlCommand("select location, description from disaster", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //object
                                ManageDisaster md = new ManageDisaster();
                                //asign
                                md.location = reader.GetString(0);
                                md.description = reader.GetString(1);
                                //store to list
                                myList.Add(md);
                            }
                        }
                    }
                }

                //get total amount
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();

                    using (SqlCommand command = new SqlCommand("select SUM(amount) from monetary", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                total = reader.GetInt32(0);
                                Console.WriteLine("Amount Avaiable is R" + total);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Failed " + err.Message);
            }
        }

        public void OnPost()
        {
            try
            {
                //get total amount
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();

                    using (SqlCommand command = new SqlCommand("select SUM(amount) from monetary", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                total = reader.GetInt32(0);
                                Console.WriteLine("Amount Avaiable is R" + total);
                            }
                        }
                    }
                }

                Console.WriteLine("DEBUG\nTotal is: " + total +"\nUser wants: " + myAllo.amount);


                if (total > myAllo.amount)
                {
                    Console.WriteLine("DEBUG");
                    //store allocated money
                    using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        //open connection
                        connect.Open();

                        //store to allocatedMoney table
                        using (SqlCommand command = new SqlCommand("insert into allocateMoney values('" + myAllo.disaster + "', '" + myAllo.date + "', " + myAllo.amount + ") ", connect))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Stored to allocate Money");
                        }

                        //store to monetary table
                        using (SqlCommand command = new SqlCommand("insert into monetary values('allocated money', '" + myAllo.date + "', " + (myAllo.amount * -1) + ") ", connect))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Stored to monetary");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed due to insuffient funds");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Failed: " + err.Message);
            }
        }
    }

    public class ManageDisaster
    {
        public string location { get; set; }
        public string description { get; set; }
    }
}