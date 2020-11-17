using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public enum TypeCity
    {
        Город,
        ПГТ,
        Деревня,
        Село,
        Посёлок,
    }

    public enum TypeStreet
    {
        Аллея, Бульвар, Набережная, Улица, Переулок, Проезд, Проспект,
    }


    public class Address
    {
        [Key]
        public int Id { get; set; }
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
