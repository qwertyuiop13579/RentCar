using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class CreateCarViewModel
    {
        public string Model { get; set; }
        public string Mark { get; set; }
        public string Color { get; set; }
        public string Goverment_number { get; set; }
        public int Year { get; set; }
        public int id_supplier { get; set; }
        public double Price { get; set; }
        public string status { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public IFormFile Image { get; set; }
    }
}
