using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Models
{
    public class Purchase
    {
        public string username { get; set; }
        public string date { get; set; }
        public int numberOfItems { get; set; }
        public int pricePerUnit { get; set; }
        public string category { get; set; }
        public string description { get; set; }
    }
}
