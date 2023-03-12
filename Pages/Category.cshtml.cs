using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class CategoryModel : PageModel
    {
        //Model
        [BindProperty]
        public CategoryDonations myCategory { get; set; }

        public void OnGet()
        {

        }

        //handling the post
        public IActionResult OnPost()
        {
            Console.WriteLine("Found Value\n" + myCategory.category);

            //store to the database
            myCategory.storeToDatabase(myCategory.category);

            //refresh
            return RedirectToPage("Category");
        }
    }
}