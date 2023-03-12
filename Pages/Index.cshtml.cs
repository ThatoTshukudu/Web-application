using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class IndexModel : PageModel
    {
        public Boolean failState = false;


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }



            String userType = login.validateDetails(login.email, login.hashPass(login.password));

            if (userType.Equals("admin"))
            {
                return RedirectToPage("publicUsers");
            }
            else if (userType.Equals("normal"))
            {
                return RedirectToPage("publicUsers");
            }
            else
            {
                return Page();
            }


        }

        //Model
        [BindProperty]
        public Login login { get; set; }


    }
}
