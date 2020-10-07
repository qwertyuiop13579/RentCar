using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class EditRentViewModel
    {
         public DateTime date1 { get; set; }

        public int id_client { get; set; }
        public int id_car { get; set; }
        public DateTime date2 { get; set; }
        public DateTime date3 { get; set; }
        public int price { get; set; }
        public DateTime datepay { get; set; }
        public string account_number { get; set; }
        public string payer_number { get; set; }
    }
}
