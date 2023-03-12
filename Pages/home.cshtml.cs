using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class homeModel : PageModel
    {
        //string Checker
        string Checker = "none";

        //My models
        [BindProperty]
        public MonetaryDonations md { get; set; }

        [BindProperty]
        public GoodsDonations gd { get; set; }


        //debug for confirmation dialog
        public string ViewBag { get; set; }

        public void OnGet()
        {

        }

        public void OnPostMonetary()
        {
            Console.WriteLine("Monetary");
            Console.WriteLine("Monetary: {0}, {1}, {2}", md.userName, md.date, md.amount);
            Checker = "Monetary";

            
            
        }

        public void OnPostGoods()
        {
            Console.WriteLine("Goods");
            Checker = "Goods";
        }

        public void OnPostCategory()
        {
            Console.WriteLine("Category");
            Checker = "Category";
        }

        public void OnPostDisaster()
        {
            Console.WriteLine("Disaster");
            Checker = "Disaster";
        }

        //the post method to handle the query
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //check which form was submitted
            switch (Checker)
            {
                case "Monetary":
                    
                    break;
                case "Goods":
                    Console.WriteLine("Goods: {0}, {1}, {2}, {3}, {4}", gd.userName, gd.date, gd.numberOfItems, gd.category, gd.description);
                    break;
                default:
                    Console.WriteLine("Failed to handle the POST");
                    break;
            }

            return RedirectToPage("home");
        }
    }
}