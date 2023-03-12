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
    public class allocateGoodsModel : PageModel
    {
        //property
        [BindProperty]
        public AllocateGoods myGoods { get; set; }

        //List 
        public List<tempGoods2> goods = new List<tempGoods2>();

        //List of disasters
        public List<ManageDisaster> myList = new List<ManageDisaster>();

        //monetary amount in bank
        public int total;

        public void OnGet()
        {
             try
            {
                //get the disaster location and description
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30"))
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
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30"))
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

            //try to get the categories in the database
            try
            {
                //connect
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();

                    //command
                    using (SqlCommand command = new SqlCommand("select category from category", connect))
                    {
                        //Read
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //object
                                tempGoods2 g = new tempGoods2();

                                //store in object
                                g.goodsName = reader.GetString(0);

                                //store to list
                                goods.Add(g);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);

            }
        }

        public void OnPost()
        {
            try
            {
                //save to the allocate goods table and decrese from goods 
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();

                    using (SqlCommand command = new SqlCommand("insert into allocateGoods values('" + myGoods.disaster + "', '" + myGoods.goods + "', " + myGoods.numberOfItems + ")", connect))
                    {
                        command.ExecuteNonQuery();
                    }                    

                    //subtract the number of goods in the goods table
                    using (SqlCommand command = new SqlCommand("select numberofItems from goods where category = '"+ myGoods.goods +"';", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int avaiable = reader.GetInt32(0);
                                int user = Convert.ToInt32(myGoods.numberOfItems);

                                Console.WriteLine("Amount of items: " + avaiable + "\nUser wants: " + user);

                                if (user > avaiable)
                                {
                                    Console.WriteLine("Can't process the allocation not enough inventory");
                                }
                                else
                                {
                                    int left = avaiable - user;

                                    updateData(left);
                                    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);
            }
        }

        public void updateData(int left)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30");

                conn.Open();

                new SqlCommand("update goods set numberofItems = " + left + " where category ='" + myGoods.goods + "';", conn).ExecuteNonQuery();

                Console.WriteLine("Database Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);
            }
        }
    }

    

    public class tempGoods2
    {
        public string goodsName { get; set; }
    }
}