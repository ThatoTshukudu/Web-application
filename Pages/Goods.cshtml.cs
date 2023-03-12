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
    public class GoodsModel : PageModel
    {
        //Model
        [BindProperty]
        public GoodsDonations myGoods { get; set; }

        //List 
        public List<tempGoods> goods = new List<tempGoods>();

        public void OnGet()
        {
            //try to get the categories in the database
            try
            {
                //connect
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\stress\\Task2.mdf;Integrated Security=True;Connect Timeout=30"))
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
                                tempGoods g = new tempGoods();

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

        //Handling the Post
        public IActionResult OnPost()
        {
            Console.WriteLine("Values Found for Goods\n{0}, {1}, {2}, {3}, {4}", myGoods.userName, myGoods.date, myGoods.numberOfItems, myGoods.category, myGoods.description);

            //store to the database
            myGoods.storeToDatabse(myGoods.userName, myGoods.date, Convert.ToInt16(myGoods.numberOfItems), myGoods.category, myGoods.description);

            //Refresh the page
            return RedirectToPage("Goods");
        }
    }

    public class tempGoods{
        public string goodsName { get; set; }
    }
}