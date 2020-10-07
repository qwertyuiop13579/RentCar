﻿using System;
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
    }
}