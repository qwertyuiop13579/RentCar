using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class EditSaleViewModel
    {
        public int Id { get; set; }
        public int id_client { get; set; }
        public int id_car { get; set; }
        public DateTime date2 { get; set; }
        public DateTime date3 { get; set; }
        public double rentprice { get; set; }
        public string status { get; set; }
        public double summ { get; set; }
    }
}
