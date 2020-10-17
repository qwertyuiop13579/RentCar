using Labs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class CreateSupplierViewModel
    {
        public string firmname { get; set; }
        public string unn { get; set; }



        public string country { get; set; }
        public TypeCity type1 { get; set; }
        public string city { get; set; }
        public TypeStreet type2 { get; set; }
        public string street { get; set; }
        public int numhouse { get; set; }
        public int numapartment { get; set; }

        public string index { get; set; }
        public string housephone { get; set; }
        public string mobilephone { get; set; }
        public string email { get; set; }
    }
}
