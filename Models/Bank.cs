using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string bank_name { get; set; }
        public string innclient { get; set; }
        public string bank_account { get; set; }
        public int id_address { get; set; }
        public string bank_bik { get; set; }
    }
}
