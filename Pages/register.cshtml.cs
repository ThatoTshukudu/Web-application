using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class registerModel : PageModel
    {
        public void OnGet()
        {

        }

        //Model
        [BindProperty]
        public register r { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //storing the details to database
            r.registerUser(r.email, r.hashPass(r.password));

            return RedirectToPage("index");


        }
    }
}