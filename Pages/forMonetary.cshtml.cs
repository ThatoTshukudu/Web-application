using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class testMonetaryModel : PageModel
    {
        [BindProperty]
        public MonetaryDonations MD { get; set; }


        public void OnGet()
        {

        }

        //when form is submitted
        public IActionResult OnPost()
        {   
            //print to console (debug)
            Console.WriteLine("Works Values {0}, {1}, {2}", MD.userName, MD.date, MD.amount);

            //store to the database
            MD.storeToDatabase(MD.userName, MD.date, MD.amount);          

            //refresh the page
            return RedirectToPage("forMonetary");
        }
    }
}