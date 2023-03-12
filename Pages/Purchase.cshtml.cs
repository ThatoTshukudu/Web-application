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
    public class PurchaseModel : PageModel
    {
        //Model Class
        [BindProperty]
        public Purchase myPurchase { get; set; }



        //List for category
        public List<tempGoods> goods = new List<tempGoods>();

        //The total money we have
        public int ourMoney { get; set; }

        public void OnGet()
        {
            //try to get the categories in the database
            try
            {
                //connect
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();

                    //command getting the category
                    using (SqlCommand command = new SqlCommand("select category from category", connect))
                    {
                        //Read
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //object
                                tempGoods g = new tempGoods();

                                //store in object
                                g.goodsName = reader.GetString(0);

                                //store to list
                                goods.Add(g);



                            }
                        }
                    }

                    //getting the money we have
                    using (SqlCommand command = new SqlCommand("select SUM(amount) from monetary", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //display on console
                                Console.WriteLine("Total is monetary money is  : R" + reader.GetInt32(0));

                                //storing 
                                ourMoney = reader.GetInt32(0);


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

        public IActionResult OnPost()
        {
            //record in the monetary and purchase
            try
            {
                //connection 
                SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30");

                //open
                connect.Open();

                //calculate the total
                int Total = myPurchase.numberOfItems * myPurchase.pricePerUnit;

                //store to monetary
                using (SqlCommand command = new SqlCommand("insert into monetary values('purchase', '" + myPurchase.date + "', " + (Total * -1) + ");", connect))
                {
                    command.ExecuteNonQuery();

                    Console.WriteLine("Purchase stored to Monetary");
                }

                //store to purchase
                using (SqlCommand command = new SqlCommand("insert into purchase(username, date, numberOfItems, pricePerUnit, TotalPrice, category, description) values('purchase', '" + myPurchase.date + "', " + myPurchase.numberOfItems + ", " + myPurchase.pricePerUnit + ", " + Total + ", '" + myPurchase.category + "', '" + myPurchase.description + "')", connect))
                {
                    command.ExecuteNonQuery();

                    Console.WriteLine("Purchase stored to Purchase");
                }

            
                using (SqlCommand kt = new SqlCommand("select numberofitems from goods where category = '" + myPurchase.username + "'", connect))
                {
                    using (SqlDataReader reader = kt.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //getting 
                            int letspurchase = reader.GetInt32(0);

                            //updating 
                          int  use = Convert.ToInt32( myPurchase.numberOfItems);

                            Console.WriteLine("Amount of items: " + letspurchase + "\nUser wants: " + use);

                            if(use > letspurchase)
                            {
                                Console.WriteLine("Can't process the allocation not enough inventory");
                            }
                            else
                            {
                                //increasing the goods in stock
                                int current = letspurchase - use;
                                updatePurchase(current);

                            }

                        }
                    }
                }
                return Page();


            }


            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);
                return Page();
            }
        }

        public void updatePurchase(int current)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                new SqlCommand("update goods set numberofitems = " + current + " where username = '" + myPurchase.username + "'", conn).ExecuteNonQuery();

                {
                  
                    Console.WriteLine("Update the inventory to " + current + " for " + myPurchase.username);


                }

            }
            catch (Exception)
            {

                throw;
            }
}
    }
}