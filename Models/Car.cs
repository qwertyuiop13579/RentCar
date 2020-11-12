using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public string Mark { get; set; }
        public string Color { get; set; }
        public string Goverment_number { get; set; }        
        public int Year { get; set; }          //год выпуска
        public int id_supplier { get; set; }
        
        public int Price { get; set; }
        public string status { get; set; }
        public string country { get; set; }
        public string city { get; set; }


        public string Image { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
