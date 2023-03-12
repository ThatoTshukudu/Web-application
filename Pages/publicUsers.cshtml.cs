using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class publicUsersModel : PageModel
    {
        public List<pMonetary> pml = new List<pMonetary>();
        public List<pGoods> pgl = new List<pGoods>();
        public List<pDisaster> pdl = new List<pDisaster>();

        public void OnGet()
        {
            //Get data
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ST10128080\\Music\\database.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=true;");
                connect.Open();

                //get the monetary
                using (SqlCommand Umd = new SqlCommand("select * from monetary where amount > 0", connect))
                {
                    SqlDataReader reader = Umd.ExecuteReader();

                    while (reader.Read())
                    {
                        pMonetary MP = new pMonetary();

                        MP.username = reader.GetString(0);
                        MP.date = reader.GetString(1);
                        MP.amount = reader.GetInt32(2);

                        pml.Add(MP);
                    }
                    reader.Close();
                }

                //get the goods
                using (SqlCommand TD = new SqlCommand("select category, SUM(numberofitems) from goods group by category", connect))
                {
                    SqlDataReader reader = TD.ExecuteReader();

                    while (reader.Read())
                    {
                        pGoods myG = new pGoods();

                        myG.category = reader.GetString(0);
                        myG.numberOfItems = reader.GetInt32(1);

                        pgl.Add(myG);
                    }
                    reader.Close();
                }

                //get the disaster
                using (SqlCommand Usd = new SqlCommand("select startDate, endDate, location, aid from disaster", connect))
                {
                    SqlDataReader reader = Usd.ExecuteReader();

                    while (reader.Read())
                    {
                        pDisaster RT = new pDisaster();

                        RT.startDate = reader.GetString(0);
                        RT.endDate = reader.GetString(1);
                        RT.location = reader.GetString(2);
                        RT.aid = reader.GetString(3);

                        //check which is active
                        DateTime current = DateTime.Today;


                        try
                        {
                            DateTime tempStartDate = Convert.ToDateTime(RT.startDate);
                            DateTime tempEndDate = Convert.ToDateTime(RT.endDate);

                            if (tempStartDate < current && current < tempEndDate)
                            {
                                Console.WriteLine("Disaster is active");

                                try
                                {
                                    //getting the number of items donated
                                    using (SqlCommand cmdGoods = new SqlCommand("select SUM(numberofitems) from allocateGoods where disaster = '" + reader.GetString(2) + "'", connect))
                                    {
                                        SqlDataReader readGoods = cmdGoods.ExecuteReader();

                                        while (readGoods.Read())
                                        {
                                            RT.PnumOfitems = readGoods.GetInt32(0);
                                        }
                                    }
                                    //getting the amount donated
                                    using (SqlCommand cmdGoods = new SqlCommand("select SUM(amount) from allocateMoney where disaster = '" + reader.GetString(2) + "'", connect))
                                    {
                                        SqlDataReader readMoney = cmdGoods.ExecuteReader();

                                        while (readMoney.Read())
                                        {
                                            RT.Mamount = readMoney.GetInt32(0);
                                        }
                                    }
                                }
                                catch (Exception err)
                                {
                                    Console.WriteLine("Failed to get the number of items\n" + err.Message);
                                }


                                pdl.Add(RT);
                            }
                            else
                            {
                                Console.WriteLine("Disaster is not active");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to Convert Date" + ex.Message);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);
            }
        }
    }

    public class pMonetary
    {
        public String username;
        public String date;
        public int amount;
    }

    public class pGoods
    {
        public String category;
        public int numberOfItems;
    }

    public class pDisaster
    {
        public String startDate;
        public String endDate;
        public String location;
        public String aid;
        public int PnumOfitems;
        public int Mamount;
    }
}
