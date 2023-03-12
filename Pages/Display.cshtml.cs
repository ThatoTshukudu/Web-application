using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class DisplayModel : PageModel
    {
        public List<MonetaryInfo> monetaryList = new List<MonetaryInfo>();
        public List<GoodsInfo> goodsList = new List<GoodsInfo>();
        public List<DisasterInfo> disasterList = new List<DisasterInfo>();
        public List<AllocateGoodsInfo> allocateGoodsList = new List<AllocateGoodsInfo>();
        public List<AllocateMoneyInfo> allocateMoneyList = new List<AllocateMoneyInfo>();
        public List<PurchasesInfo> purchasesList = new List<PurchasesInfo>();

        public void OnGet()
        {
            //get data from database
            try
            {
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();

                    //Getting the monetary info
                    string sql = "select * from monetary";
                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //object and get
                                MonetaryInfo info = new MonetaryInfo();
                                info.userName = reader.GetString(0);
                                info.date = reader.GetString(1);
                                info.amount = "" + reader.GetInt32(2);

                                //add object to list
                                monetaryList.Add(info);
                            }
                        }
                    }

                    //Getting the goods
                    using (SqlCommand command = new SqlCommand("select * from goods", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoodsInfo temp = new GoodsInfo();

                                temp.userName = reader.GetString(0);
                                temp.date = reader.GetString(1);
                                temp.numberOfItems = reader.GetInt32(2);
                                temp.category = reader.GetString(3);
                                temp.description = reader.GetString(4);

                                goodsList.Add(temp);
                            }
                        }
                    }


                    //Getting the disasters
                    using (SqlCommand command = new SqlCommand("select * from disaster", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisasterInfo temp = new DisasterInfo();

                                temp.startDate = reader.GetString(0);
                                temp.endDate = reader.GetString(1);
                                temp.location = reader.GetString(2);
                                temp.description = reader.GetString(3);
                                temp.aid = reader.GetString(4);

                                disasterList.Add(temp);
                            }
                        }
                    }

                    //Getting the allocated goods
                    using (SqlCommand command = new SqlCommand("select * from allocateGoods", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AllocateGoodsInfo temp = new AllocateGoodsInfo();

                                temp.disaster = reader.GetString(0);
                                temp.goods = reader.GetString(1);
                                temp.items = reader.GetInt32(2);

                                allocateGoodsList.Add(temp);
                            }
                        }
                    }

                    //Getting the allocated money
                    using (SqlCommand command = new SqlCommand("select * from allocateMoney", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AllocateMoneyInfo temp = new AllocateMoneyInfo();

                                temp.disaster = reader.GetString(0);
                                temp.date = reader.GetString(1);
                                temp.amount = Convert.ToInt32(reader.GetInt32(2));

                                allocateMoneyList.Add(temp);
                            }
                        }
                    }

                            //public string date;
                            //public int items;
                            //public int pricePerItem;
                            //public int totalPrice;
                            //public string category;

                    //Getting the purchase
                    using (SqlCommand command = new SqlCommand("select date, numberOfItems, pricePerUnit, TotalPrice, category from purchase", connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PurchasesInfo temp = new PurchasesInfo();

                                temp.date = reader.GetString(0);
                                temp.items = reader.GetInt32(1);
                                temp.pricePerItem = reader.GetInt32(2);
                                temp.totalPrice = reader.GetInt32(3);
                                temp.category = reader.GetString(4);

                                purchasesList.Add(temp);
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
    }

    //class for the monetary donations
    public class MonetaryInfo
    {
        public string userName;
        public string date;
        public string amount;
    }

    //class for the goods donations
    public class GoodsInfo
    {
        public string userName;
        public string date;
        public int numberOfItems;
        public string category;
        public string description;
    }


    //class for the disaster donations
    public class DisasterInfo
    {
        public string startDate;
        public string endDate;
        public string location;
        public string description;
        public string aid;
    }

    //class for the allocate goods
    public class AllocateGoodsInfo
    {
        public string disaster;
        public string goods;
        public int items;
    }

    //class for the allocate money
    public class AllocateMoneyInfo
    {
        public string disaster;
        public string date;
        public int amount;
    }

    //class for the purchases
    public class PurchasesInfo
    {
        public string date;
        public int items;
        public int pricePerItem;
        public int totalPrice;
        public string category;
    }


}