using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public enum TypeCity
    {
        город,
        деревня,
        ПГТ,
        посёлок
    }

    public enum TypeStreet
    {
        улица,
        проспект,
        проезд,
        переулок,
        бульвар,
    }


    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int id_country { get; set; }
        public int id_region { get; set; }
        public int id_district { get; set; }
        public TypeCity type1 { get; set; }
        public int id_city { get; set; }
        public TypeStreet type2 { get; set; }

        public int id_street { get; set; }

        public string num1 { get; set; }
        public int num2 { get; set; }
        public string num3 { get; set; }
        public int index { get; set; }
        public int num4 { get; set; }
        public int code { get; set; }
        public int mobile { get; set; }
        public string email { get; set; }
    }
}
