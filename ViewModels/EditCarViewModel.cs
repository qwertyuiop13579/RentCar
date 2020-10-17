using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class EditCarViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Mark { get; set; }
        public string Color { get; set; }
        public string Goverment_number { get; set; }
        public int Year { get; set; }
        public int id_supplier { get; set; }
        public int Price { get; set; }
        public string status { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }
}
