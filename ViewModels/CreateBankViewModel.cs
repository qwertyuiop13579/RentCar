using Labs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class CreateBankViewModel
    {
        public string bank_name { get; set; }
        public string innclient { get; set; }
        public string bank_account { get; set; }
        public int id_address { get; set; }
        public string bank_bik { get; set; }


        public string country { get; set; }
        public string region { get; set; }
        public string district { get; set; }
        public TypeCity type1 { get; set; }
        public string city { get; set; }
        public TypeStreet type2 { get; set; }

        
        public string street { get; set; }

        [MaxLength(4)]
        public string num1 { get; set; }
        public int num2 { get; set; }
        [MaxLength(5)]
        public string num3 { get; set; }

        public int index { get; set; }
        public int num4 { get; set; }
        public int code { get; set; }
        public int mobile { get; set; }
        public string email { get; set; }

    }
}
