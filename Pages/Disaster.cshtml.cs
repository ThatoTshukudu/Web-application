using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation.Pages
{
    public class DisasterModel : PageModel
    {
        //Model
        [BindProperty]
        public DisasterCapture myDisaster { get; set; }

        public void OnGet()
        {

        }

        //handle post
        public IActionResult OnPost()
        {
            //debug
            Console.WriteLine("Found Values\n:{0}, {1}, {2}, {3}, {4}", myDisaster.startDate, myDisaster.endDate, myDisaster.location, myDisaster.description, myDisaster.aid);

            //store to the database
            myDisaster.storeToDatabse(myDisaster.startDate, myDisaster.endDate, myDisaster.location, myDisaster.description, myDisaster.aid);

            //refresh
            return RedirectToPage("Disaster");
        }
    }
}